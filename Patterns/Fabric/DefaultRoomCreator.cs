namespace Patterns;

public class DefaultRoomCreator : RoomCreator
{
    public override IRoom CreateRoom() => new Room();
}