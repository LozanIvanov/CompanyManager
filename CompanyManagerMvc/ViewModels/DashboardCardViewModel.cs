namespace CompanyManagerMvc.ViewModels
{
    public class DashboardCardViewModel
    {
        public string Title { get; set; } = string.Empty;

        public int Count { get; set; }

        public string Controller { get; set; } = string.Empty;

        public string ButtonText { get; set; } = string.Empty;

        public string Icon { get; set; } = string.Empty;

        public string ButtonClass { get; set; } = "btn-primary";
    }
}
