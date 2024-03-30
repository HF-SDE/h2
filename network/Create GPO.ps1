# Import the required PowerShell module for Active Directory
Import-Module ActiveDirectory

# Function to create a shared drive
function CreateSharedDrive($driveLetter, $path) {
    # Create the shared drive if it doesn't exist
    if (-not (Test-Path $path -PathType Container)) {
        New-Item -Path $path -ItemType Directory
    }

    # Map the drive for each user in the specified security group
    $securityGroups | ForEach-Object {
        $groupName = $_.SamAccountName
        $drivePath = "$path\$groupName"
        $accessRule = $accessMatrix[$groupName]

        # Grant permissions based on the access matrix
        Grant-Permission -Path $drivePath -AccessRule $accessRule
    }
}

# Function to grant permissions on a folder
function Grant-Permission {
    param(
        [string]$Path,
        [string]$AccessRule
    )

    # Define access rules
    $permissions = @{
        "F"  = "FullControl"
        "RW" = "Modify"
        "R"  = "Read"
    }

    # Grant permissions
    $acl = Get-Acl $Path
    $rule = New-Object System.Security.AccessControl.FileSystemAccessRule(
        "Domain Admins", $permissions[$AccessRule], "ContainerInherit,ObjectInherit", "None", "Allow"
    )
    $acl.AddAccessRule($rule)
    Set-Acl $Path $acl
}

# Function to Configure Password Policy
function ConfigurePasswordPolicy {
    # Set the password policy settings in the GPO
    # Get the Default Domain Policy GPO
    $defaultDomainPolicy = Get-GPO -Name "Default Domain Policy"

    # Configure GPO settings
    foreach ($setting in $passwordSettings.GetEnumerator()) {
        $name = $setting.Key
        $value = $setting.Value
        Set-GPRegistryValue -Guid $defaultDomainPolicy.Id -Key "HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System" -ValueName $name -Type DWORD -Value $value
    }

    # Force a Group Policy update
    gpupdate /force
}

# Define the password policy settings
$passwordSettings = @{
    "MinPasswordLength"    = 10
    "PasswordComplexity"   = $true
    "PasswordHistoryCount" = 25
    "MaxPasswordAge"       = (New-TimeSpan -Days 25).Ticks / 10000000
}

# Define shared drive paths
$sharedDrives = @(
    "\\MBFILSRV-V01\it-mappe",
    "\\MBFILSRV-V01\administration",
    "\\MBFILSRV-V01\revision"
)

# Define security groups
$securityGroups = @(
    [PSCustomObject]@{ SamAccountName = "GSAdmins" },
    [PSCustomObject]@{ SamAccountName = "GSIT" },
    [PSCustomObject]@{ SamAccountName = "GSEXTERNEL" },
    [PSCustomObject]@{ SamAccountName = "GSREVISOR" },
    [PSCustomObject]@{ SamAccountName = "GSADMINISTRATION" }
)

# Define access matrix
$accessMatrix = @{
    "GSAdmins"         = "F"
    "GSIT"             = "RW"
    "GSEXTERNEL"       = "F,R"
    "GSREVISOR"        = "F,RW"
    "GSADMINISTRATION" = "F,RW"
}

# Define security groups
$securityGroups = @(
    "GSAdmins",
    "GSIT",
    "GSEXTERNEL",
    "GSREVISOR",
    "GSADMINISTRATION"
)

# Loop through security groups and assign permissions to shared drives
foreach ($group in $securityGroups) {
    foreach ($drive in $sharedDrives) {
        $permission = switch ($drive) {
            "\\MBFILSRV-V01\it-mappe" { "F" }
            "\\MBFILSRV-V01\administration" { if ($group -eq "GSADMINISTRATION") { "RW" } else { "F" } }
            "\\MBFILSRV-V01\revision" { if ($group -eq "GSREVISOR") { "RW" } else { "R" } }
        }

        # Create shared drive
        New-Item -Path $drive -ItemType Directory

        # Grant permissions to security group on the shared drive
        $acl = Get-Acl $drive
        $permissionRule = New-Object System.Security.AccessControl.FileSystemAccessRule("$group", $permission, "ContainerInherit,ObjectInherit", "None", "Allow")
        $acl.AddAccessRule($permissionRule)
        Set-Acl -Path $drive -AclObject $acl
    }
}

Write-Host "Script execution completed successfully."
