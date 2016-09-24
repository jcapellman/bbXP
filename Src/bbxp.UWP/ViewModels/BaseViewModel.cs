﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using bbxp.PCL.Settings;
using bbxp.PCL.Common;
using bbxp.PCL.Handlers;

using bbxp.UWP.PlatformImplementations;

namespace bbxp.UWP.ViewModels {
    public class BaseViewModel : INotifyPropertyChanged {
        public GlobalSettings GSettings = new GlobalSettings {
            WebAPIAddress = "http://localhost:1337/api/",
            CachingWebAPIAddress = "http://localhost:1338/node/",
            WebAddress = "http://localhost:1025/"
        };

        protected LocalStorage _localStorage;

        public BaseViewModel() { _localStorage = new LocalStorage(); }

        public async Task<ReturnSet<string>> GetCSSContent() {
            var localFile = await _localStorage.ReadFile<string>("CSSContent");

            if (!string.IsNullOrEmpty(localFile)) {
                return new ReturnSet<string>(localFile);
            }

            var cssContentHandler = new CSSContentHandler();

            var cssContent = await cssContentHandler.GetCSSContent(GSettings.WebAddress);

            if (string.IsNullOrEmpty(cssContent)) {
                return new ReturnSet<string>(new Exception("Could not read local css content or remote content"));
            }

            var result = await _localStorage.WriteFile<string>("CSSContent", cssContent);

            if (result.HasError) {
                return new ReturnSet<string>(new Exception(result.ExceptionMessage));
            }

            return new ReturnSet<string>(cssContent);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}