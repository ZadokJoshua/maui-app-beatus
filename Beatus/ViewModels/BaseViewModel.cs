using CommunityToolkit.Mvvm.ComponentModel;

namespace Beatus.ViewModels;

public abstract partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    private bool isBusy;
}
