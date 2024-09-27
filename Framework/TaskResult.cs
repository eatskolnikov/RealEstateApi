public class TaskResult{
    public string Message {get; private set;}
    public bool Success {get; private set;}

    public TaskResult(bool success, string message){
        Message = message;
        Success = success;
    }
}

public class TaskResult<T> : TaskResult {
    public T? Data {get; private set; }

    public TaskResult(bool success, string message, T? data) : base(success, message){
        Data = data;
    }
}