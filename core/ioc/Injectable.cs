namespace webapi.core.ioc;

using System;

[AttributeUsage(AttributeTargets.Class)]
public class Injectable : Attribute
{
    public InjectType InjectType { get; init; }
    public Injectable(InjectType injectType = InjectType.Scoped) => InjectType = injectType;
}