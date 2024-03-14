using System;

[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
public class ComponentDependencyAttribute : Attribute
{
    public Type RequiredComponentType { get; private set; }

    public ComponentDependencyAttribute(Type requiredComponentType)
    {
        RequiredComponentType = requiredComponentType;
    }
}