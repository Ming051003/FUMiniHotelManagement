
using BusinessObjects.Models;
using DataAccessLayer;
using DataAccessLayer.DTO;
using Repositories.Interface;

namespace Repositories
{
    public class RoomRepository : IRoomRepository
    {
        public async Task AddRoom(RoomDTO room) => await RoomDAO.AddRoom(room);

        public async Task DeleteRoom(int roomId) => await RoomDAO.DeleteRoom(roomId);

        public List<RoomDTO> GetRooms(Func<RoomInformation, bool> predicate) => RoomDAO.GetRooms(predicate);

        public async Task UpdateRoom(RoomDTO room) => await RoomDAO.UpdateRoom(room);

        public int CountRooms() => RoomDAO.CountRooms();
    }
}
