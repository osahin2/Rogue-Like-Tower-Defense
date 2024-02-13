﻿namespace Service_Locator
{
    public interface IServiceLocator
    {
        void Register<T>(T service) where T : class;
        T Get<T>() where T : class;
        bool TryGet<T>(out T service) where T : class;
        bool IsRegistered<T>();
    }
}