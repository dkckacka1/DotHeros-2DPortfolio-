/*
 * Undo 시스템을 위한 Undo 인터페이스
 */

namespace Portfolio.Lobby
{
    public interface IUndoable
    {
        public void Undo();
    }
}
