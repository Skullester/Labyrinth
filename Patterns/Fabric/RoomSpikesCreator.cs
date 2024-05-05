namespace Patterns;

public class RoomSpikesCreator : RoomCreator
{
    public override IRoom CreateRoom() => new RoomWithSpikes();
}