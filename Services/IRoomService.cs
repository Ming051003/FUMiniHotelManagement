using BusinessObjects.Models;
using DataAccessLayer.DTO;

namespace Services.Interface
{
    public interface IRoomService
    {
        List<RoomDTO> GetRooms(Func<RoomInformation, bool> predicate);
        Task AddRoom(RoomDTO room);
        Task UpdateRoom(RoomDTO room);
        Task DeleteRoom(int roomId);
        int CountRooms();
    }
}
