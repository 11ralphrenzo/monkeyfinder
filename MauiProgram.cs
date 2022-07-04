using monkeyfinder.Pages;
﻿using monkeyfinder.Services;
using monkeyfinder.ViewModel;

namespace monkeyfinder;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		// Services 
		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<MonkeysViewModel>();
		builder.Services.AddTransient<DetailsPage>();
        builder.Services.AddSingleton<MonkeyDetailsViewModel>();
		builder.Services.AddSingleton<MonkeyService>();
		builder.Services.AddSingleton<MonkeysViewModel>();

		return builder.Build();
	}
}
