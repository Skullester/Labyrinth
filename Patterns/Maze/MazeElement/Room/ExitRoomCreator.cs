namespace Patterns;

public class ExitRoomCreator : RoomCreator
{
    public override IRoom CreateRoom() => new ExitRoom();
}