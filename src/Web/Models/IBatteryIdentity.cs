namespace Web.Models;

public interface IBatteryIdentity
{
    string AnsiName { get; set; }
    string IecName { get; set; }
    string Name { get; set; }
}