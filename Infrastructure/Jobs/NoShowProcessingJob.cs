
using Hangfire;
using Infrastructure.Services.ReservationServ;

namespace Infrastructure.Jobs;

public static class NoShowProcessingJob
{
    public static void Register()
    {
        RecurringJob.AddOrUpdate<IReservationService>(
            "process-no-shows",
            service => service.ProcessNoShowsAsync(),
            Cron.Hourly
        );
    }
}