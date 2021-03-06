﻿namespace WebAPI.JWTAuth.Template.BackgroundWorker.Cron
{
    /// <summary>
    /// Code kindly provided by Maarten Balliauw
    /// https://blog.maartenballiauw.be/post/2017/08/01/building-a-scheduled-cache-updater-in-aspnet-core-2.html
    /// </summary>
    public delegate void CrontabFieldAccumulator(int start, int end, int interval);
}