using System.Threading.Tasks;

namespace GameState
{
    public interface IGameState
    {
        void Enter();
        void Exit();
    }
}