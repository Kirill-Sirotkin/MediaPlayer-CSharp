﻿using MediaApp.Application;

internal class Program
{
    private static void Main(string[] args)
    {
        App App = App.Instance;
        App.Start();
    }
}