using System.Threading.Tasks;

namespace GameState
{
    public interface IGameState
    {
        Task Enter();
        Task Exit();
    }
}