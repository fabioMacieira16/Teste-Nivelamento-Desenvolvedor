namespace Questao5.Infrastructure.CrossCutting;


public enum Priority
{
    High = 1,
    Average = 2,
    Low = 3
}

public enum Layer
{
    App = 1,
    Domain = 2,
    Repository = 3,
    Others = 4
}
public enum TypeNotificationNoty
{
    Alert = 1,
    Error = 2,
    Sucess = 3,
    Information = 4
}

public enum NotyIntention
{

}


public class Notifications
{
    public Priority Priority { get; set; }

    public Layer? Layer { get; set; }

    public TypeNotificationNoty TypeNotificationNoty { get; set; }

    public string Message { get; set; }

    public NotyIntention? NotyIntention { get; set; }

    public List<string> PropertsErrors { get; set; }

    public Notifications()
    {
        Priority = Priority.Average;
        TypeNotificationNoty = TypeNotificationNoty.Error;
        PropertsErrors = new List<string>();
    }
}

public class LogNotifications : List<Notifications>
{

    new void Add(Notifications not)
    {

        base.Add(not);
    }
}