# Modul for AD cmdlets funtioner
Import-Module ActiveDirectory

# Get Current Dir
$selectedDirectory = Get-Location | Select-Object -ExpandProperty Path

#Get OR
$RootOU = Get-ADOrganizationalUnit -Filter * | Select-Object DistinguishedName
$RootOU = $RootOU[0].DistinguishedName
#Oprettelse af OU\er                                                                  # MUNKEBJERG -> USERS + SECURITYGROUPS + ADMINS + SERVICEUSERS
# -> Administration + Revisor
New-ADOrganizationalUnit -Name "MUNKEBJERG" -Path "$RootOU" 
New-ADOrganizationalUnit -Name "USERS" -Path "OU=MUNKEBJERG,$RootOU"
New-ADOrganizationalUnit -Name "SECURITYGROUPS" -Path "OU=MUNKEBJERG,$RootOU"
New-ADOrganizationalUnit -Name "SERVICEUSERS" -Path "OU=MUNKEBJERG,$RootOU"
New-ADOrganizationalUnit -Name "IT" -Path "OU=USERS,OU=MUNKEBJERG,$RootOU"
New-ADOrganizationalUnit -Name "ADMINISTRATION" -Path "OU=USERS,OU=MUNKEBJERG,$RootOU"
New-ADOrganizationalUnit -Name "REVISOR" -Path "OU=USERS,OU=MUNKEBJERG,$RootOU"

# Create SC Groups
$ADGroups = Import-Csv $selectedDirectory\SCGroups.csv -Delimiter ";"
# Henter data fra hver raekke i CSV fil og definere punkterne
foreach ($Group in $ADGroups) {

    #L;ser dataen fra hver r;kke og assigner dem til variablerne
    $name = $Group.name
    $samAccountName = $Group.samaccountname
    $displayName = $Group.displayname
    $description = $Group.description
    $ou = $Group.ou + "," + $RootOU #Definere hvor brugeren skal oprettes, husk rigtig path til dette.

    # Checker om brugeren allerede eksisterer for at undgaa problemer
    if (Get-ADGroup -F "SamAccountName -eq  '$samAccountName'") {
        
        # Giver warning hvis brugeren allerede eksisterer
        Write-Warning "En secrity group med navnet $name findes allerede i dit Active Directory."
    }
    else {

        # Hvis brugeren ikke findes fortsaetter den her.
        # Brugeren vil blive lavet i det OU som er angivet med $OU variable som bliver l;st fra CSV filen
        New-ADGroup `
            -Name $name `
            -SamAccountName $samAccountName `
            -GroupCategory Security `
            -GroupScope Global `
            -DisplayName $displayName `
            -Path $ou `
            -Description $description


        # Hvis brugeren oprettes vises dette i Terminialen
        Write-Host "Secrity gruppen - $name blev oprettet." -ForegroundColor Cyan
    }
}

# Create users
# Importer data fra newusers.csv fil og definer som $ADUsers
$ADUsers = Import-Csv $selectedDirectory\MR-USERS-OPRETTELSE.csv -Delimiter ";"

# Definere UPN/Firmanavn // om det er .local eller .dk
$UPN = "munkebjerg.dk"
# Henter data fra hver raekke i CSV fil og definere punkterne
foreach ($User in $ADUsers) {

    #L;ser dataen fra hver r;kke og assigner dem til variablerne
    $username = $User.username
    $password = $User.password
    $firstname = $User.firstname
    $lastname = $User.lastname
    $initials = $User.initials
    $OU = $User.ou + "," + $RootOU #Definere hvor brugeren skal oprettes, husk rigtig path til dette.
    $email = $User.username + "@" + $User.Emaildomain
    $streetaddress = $User.streetaddress
    $city = $User.city
    $zipcode = $User.zipcode
    $state = $User.state
    $country = $User.country
    $telephone = $User.telephone
    $company = $User.company
    $department = $User.department
    $description = $User.description 
    $scgroup = $User.scgroup

    # Checker om brugeren allerede eksisterer for at undgaa problemer
    if (Get-ADUser -F "SamAccountName -eq '$username'") {
        
        # Giver warning hvis brugeren allerede eksisterer
        Write-Warning "En bruger med brugernavnet $username findes allerede i dit Active Directory."
    }
    else {

        # Hvis brugeren ikke findes fortsaetter den her.
        # Brugeren vil blive lavet i det OU som er angivet med $OU variable som bliver l;st fra CSV filen
        New-ADUser `
            -SamAccountName $username `
            -UserPrincipalName "$username@$UPN" `
            -Name "$firstname $lastname" `
            -GivenName $firstname `
            -Surname $lastname `
            -Initials $initials `
            -Enabled $True `
            -DisplayName "$lastname, $firstname" `
            -Path $OU `
            -City $city `
            -PostalCode $zipcode `
            -Country $country `
            -Company $company `
            -State $state `
            -StreetAddress $streetaddress `
            -OfficePhone $telephone `
            -EmailAddress $email `
            -Department $department `
            -Description $description `
            -AccountPassword (ConvertTo-SecureString $password -AsPlainText -Force) `
            -ChangePasswordAtLogon $False `
            -PasswordNeverExpires $True `
            -CannotChangePassword $True


        # Hvis brugeren oprettes vises dette i Terminialen
        Write-Host "Brugeren - $username blev oprettet." -ForegroundColor Cyan

        # Add user to group
        Add-ADGroupMember -Identity $scgroup -Members $username

        # Hvis brugeren er sat i gruppen vises dette i Terminialen
        Write-Host "Brugeren - $username er add til gruppen $scgroup." -ForegroundColor Cyan
    }
}

Read-Host -Prompt "Brugere og grupper oprettet, Tryk ENTER for at fortsaette"