namespace Grayson.ExampleCQRS.Domain.Model
{
    public class Adres : EventSourcedAggregate
    {
        public int Huisnummer
        {
            get;
            private set;
        }

        public string Postcode
        {
            get;
            private set;
        }

        public Adres(IMessgeBus serviceBus): base(serviceBus)
        {
        }
    }

    public class AdresView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        // Raise a property change notification
        protected virtual void OnPropertyChanged(string propname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }

        private ObservableCollection<Adres> _Adress;
        public ObservableCollection<Adres> Adress
        {
            get
            {
                return _Adress;
            }

            set
            {
                _Adress = value;
                OnPropertyChanged(nameof(Adress));
            }
        }

        public AdresView()
        {
        // Implement your logic to load a collection of items
        }
    }
}