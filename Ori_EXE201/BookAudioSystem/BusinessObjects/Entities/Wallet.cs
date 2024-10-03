namespace BookAudioSystem.BusinessObjects.Entities
{
    public class Wallet
    {
        public int WalletID { get; set; }
        public int UserID { get; set; }
        public decimal Balance { get; set; }

        // Navigation property
        public User User { get; set; }
    }
}
