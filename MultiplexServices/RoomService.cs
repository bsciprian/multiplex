using MultiplexData;
using MultiplexData.Models;
using MultiplexServices.Models.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiplexServices
{
    public class RoomService : ServiceBase<RoomModel, Room>
    {

        public RoomService(MultiplexDbContext context):base(context)
        {
           
        }

        public override Room FromModel(RoomModel model)
        {
            return new Room {Id= model.Id, RoomName = model.RoomName, SeatsNumber = model.Seats };
        }

        public RoomModel ToModel(Room room)
        {
            return new RoomModel { RoomName = room.RoomName, Seats = room.SeatsNumber, Id = room.Id };
        }

        public RoomIndexModel GetAll()
        {
           var model = new RoomIndexModel()
           {
               Rooms = DbContext.Rooms.Select(x => this.ToModel(x)).ToList()
           };
            
            return model;
        }

        public RoomModel GetById(int id)
        {
            return ToModel(DbContext.Rooms
                .FirstOrDefault(m => m.Id == id));
        }

        public new void Add(RoomModel roomModel)
        {
            var entity = FromModel(roomModel);
            DbContext.Add(entity);
            var rows = entity.SeatsNumber.Split(',').Select(Int32.Parse).ToList();
            int letter = 65;
            for (int i = 0; i < rows.Count; i++)
            {;
                for (int j = 0; j < rows[i]; j++)
                {
                    var seatRoom = new SeatRoom { RoomId = entity.Id, SeatName = ((Char)(letter)).ToString()
                        + (j+1).ToString() };
                    DbContext.Add(seatRoom);
                }
                letter++;
            }
            DbContext.SaveChanges();
        }
    }
}
