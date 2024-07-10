using NewsAPI.Errors;
using NewsAPI.Interfaces;

namespace NewsAPI.Repositories;

public class UnitOfWork(
    Context context,
    IAuthRepository authRepository,
    INewsRepository newsRepository,
    ILikesRepository likesRepository,
    IUploadRepository uploadRepository,
    IUserRepository userRepository
) : IUnitOfWork
{
    public IAuthRepository AuthRepository => authRepository;

    public INewsRepository NewsRepository => newsRepository;

    public ILikesRepository LikesRepository => likesRepository;

    public IUploadRepository UploadRepository => uploadRepository;

    public IUserRepository UserRepository => userRepository;

    public async Task Complete()
    {
        if (await context.SaveChangesAsync() > 0) return;
        throw new AppException("Failed to save changes", 500);
    }

    public bool HasChanges()
    {
        return context.ChangeTracker.HasChanges();
    }
}
