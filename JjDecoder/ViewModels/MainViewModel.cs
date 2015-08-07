using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using JjDecoder.Providers;

namespace JjDecoder.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private string jsCode;
        private string decodedjsCode;
        
        public MainViewModel()
        {
            DeobfuscateCommand = new RelayCommand(DeobfuscateExecute, DeobfuscateCanExecute);
        }

        private bool DeobfuscateCanExecute()
        {
            return !string.IsNullOrEmpty(JsCode);
        }

        public RelayCommand DeobfuscateCommand { get; private set; }

        public string JsCode
        {
            get
            {
                return jsCode;
            }
            set
            {
                if (value != jsCode)
                {
                    jsCode = value;
                    DeobfuscateCommand.RaiseCanExecuteChanged();
                    base.RaisePropertyChanged();
                }
            }
        }

        public string DecodedJsCode
        {
            get
            {
                return decodedjsCode;
            }
            set
            {
                if (value != decodedjsCode)
                {
                    decodedjsCode = value;
                    base.RaisePropertyChanged();
                }
            }
        }

        private void DeobfuscateExecute()
        {
            DecodedJsCode = string.Empty;

            var decodeResult = JjDecoderProvider.Decode(jsCode);

            if (string.IsNullOrEmpty(decodeResult))
            {
                DecodedJsCode = "JavaScript not detected";
            }
            else
            {
                DecodedJsCode = decodeResult;
            }
        }
    }
}