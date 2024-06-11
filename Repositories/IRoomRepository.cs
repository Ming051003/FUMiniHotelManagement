
using BusinessObjects.Models;
using DataAccessLayer.DTO;

namespace Repositories.Interface
{
    public interface IRoomRepository
    {
        List<RoomDTO> GetRooms(Func<RoomInformation, bool> predicate);
        Task AddRoom(RoomDTO room);
        Task UpdateRoom(RoomDTO room);
        Task DeleteRoom(int roomId);
        int CountRooms();
    }
}
