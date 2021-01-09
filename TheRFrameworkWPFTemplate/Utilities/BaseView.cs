namespace TheRFramework.Utilities
{
    /// <summary>
    /// An interface that all views should implement if they wish to use data binding
    /// </summary>
    /// <typeparam name="ViewModel">The ViewModel class that implements <see cref="BaseViewModel"/></typeparam>
    public interface BaseView<ViewModel> where ViewModel : BaseViewModel
    {
        ViewModel Model { get; }
    }
}
