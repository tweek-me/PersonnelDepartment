namespace PersonnelDepartment.Infrastructure;

public readonly struct ReactApp
{
    public String Name { get; }
    public String ContainerId { get; }

    public ReactApp(String name, String containerId = "root")
    {
        Name = name;
        ContainerId = containerId;
    }
}