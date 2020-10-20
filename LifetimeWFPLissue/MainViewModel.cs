using LifetimeWFPLissue.MVVM;

namespace LifetimeWFPLissue
{
    public class MainViewModel : ViewModelBase
    {
        public void OpenChild()
        {
            App.ShowNonModal(new ChildViewModel(), typeof(ChildWindow));
        }
    }
}