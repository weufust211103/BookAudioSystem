using BookAudioSystem.BusinessObjects.Entities;

namespace BookAudioSystem.Services.IService
{
    public interface IVNPayService
    {
        string GenerateVNPayQRCodeUrl(Transaction transaction);
    }
}
