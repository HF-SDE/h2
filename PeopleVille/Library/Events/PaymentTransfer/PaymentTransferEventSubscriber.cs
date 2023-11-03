using Library.Interfaces;
using Library.Records;
using Library.Utils;
using System.Configuration;

namespace Library.Events.PaymentTransfer
{
    internal class PaymentTransferEventSubscriber : IPaymentTransferEvent
    {
        public void Subscribe(PaymentTransferEventPublisher publisher)
        {
            publisher.PaymentTransferEvent += OnPaymentTransferEvent;
        }

        public void Unsubscribe(PaymentTransferEventPublisher publisher)
        {
            publisher.PaymentTransferEvent -= OnPaymentTransferEvent;
        }

        public void OnPaymentTransferEvent(object sender, PaymentTransferEventArgs e)
        {
            string senderUuid = e.EntityUUID;
            string receiverUuid = e.ToUUID;
            TravelTime travelTime = new();
            travelTime.Generate(0, 23);

            FileClass file = new();
            string fileName = ConfigurationManager.AppSettings["peopleDataFileName"]!;
            List<RPeople> data = file.Get<RPeople>(fileName);
            Logger logger = new();
            Day day = new();
            string text;

            int senderIndex = data.FindIndex(person => person.UUID == senderUuid);
            float senderCoins = data.First(person => person.UUID == senderUuid).Coins;
            string senderHome = data.First(person => person.UUID == senderUuid).Home;
            List<RLogs> senderLogs = data.First(person => person.UUID == senderUuid).Logs;
            List<RItem> senderInventory = data.First(person => person.UUID == senderUuid).Inventory;
            if(senderCoins < e.Amount)
            {
                text = $"Payment canceled not enough money. Sender: {senderUuid} Amount: {e.Amount} Receiver: {e.ToUUID} ";
                Console.WriteLine($"{day.ToString()} day {travelTime.FromClock}: {text}");
                logger.Log(fileName, senderUuid, day.Get(), travelTime.FromClock, text);
                return;
            }
            senderCoins -= e.Amount;

            data.RemoveAt(senderIndex);
            data.Insert(senderIndex, new() { Coins = senderCoins, Home = senderHome, Inventory = senderInventory, Logs = senderLogs, UUID = senderUuid });

            int receiverIndex = data.FindIndex(person => person.UUID == receiverUuid);
            float receiverCoins = data.First(person => person.UUID == receiverUuid).Coins + e.Amount;
            string receiverHome = data.First(person => person.UUID == receiverUuid).Home;
            List<RLogs> receiverLogs = data.First(person => person.UUID == receiverUuid).Logs;
            List<RItem> receiverInventory = data.First(person => person.UUID == receiverUuid).Inventory;
            data.RemoveAt(receiverIndex);
            data.Insert(receiverIndex, new() { Coins = receiverCoins, Home = receiverHome, Inventory = receiverInventory, Logs = receiverLogs, UUID = receiverUuid });
            file.SaveHelper(fileName, data);


            text = $"{senderUuid} payed {e.Amount} to {e.ToUUID}";
            Console.WriteLine($"{day} day {travelTime.FromClock}: {text}");
            logger.Log(fileName, senderUuid, day.Get(), travelTime.FromClock, text);
        }
    }
}
