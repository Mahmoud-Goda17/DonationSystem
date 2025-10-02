using DonationSystem.API.Middlewares;

namespace DonationSystem.API.MainExtention
{
    public static class ApplicationBuilderExtentions
    {
        public static WebApplication UseApplicationPipeline(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
            return app;
        }
    }
}
