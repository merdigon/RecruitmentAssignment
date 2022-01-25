namespace RecruitmentAssignment.Authentication
{
    public interface IAppSettingsHandler
    {
        /// <summary>
        /// Get Api Key for authentication
        /// </summary>
        /// <returns></returns>
        string GetApiKey();
    }
}
