namespace NewsAPI.Interfaces;

public interface IUnitOfWork
{

    IAuthRepository AuthRepository { get; }
    INewsRepository NewsRepository { get; }
    ILikesRepository LikesRepository { get; }
    IUploadRepository UploadRepository { get; }
    IUserRepository UserRepository { get; }
    Task Complete();
    bool HasChanges();
}
