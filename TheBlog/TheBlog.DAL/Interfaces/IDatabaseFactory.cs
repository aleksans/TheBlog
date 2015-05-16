namespace TheBlog.DAL.Interfaces
{
    public interface IDatabaseFactory
    {
        BlogContext Get();
    }
}
