// Updated by XamlIntelliSenseFileGenerator 2024-12-10 16:39:11
#pragma checksum "D:\SESSION 3\PROG INTERFACE\GitHub\projet_prog\prog_final\prog_final\pageAffichageAdherent.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "B1A627D393717070F4A138F79851EC4024996F55BC701B52D805EF0CFF5CDCE0"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace prog_final
{
    partial class pageAffichageAdherent : global::Microsoft.UI.Xaml.Controls.Page
    {
#pragma warning restore 0649
#pragma warning restore 0169
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2408")]
        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2408")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent()
        {
            if (_contentLoaded)
                return;

            _contentLoaded = true;

            global::System.Uri resourceLocator = new global::System.Uri("ms-appx:///pageAffichageAdherent.xaml");
            global::Microsoft.UI.Xaml.Application.LoadComponent(this, resourceLocator, global::Microsoft.UI.Xaml.Controls.Primitives.ComponentResourceLocation.Application);
        }

        partial void UnloadObject(global::Microsoft.UI.Xaml.DependencyObject unloadableObject);

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2408")]
        private interface IpageAffichageAdherent_Bindings
        {
            void Initialize();
            void Update();
            void StopTracking();
            void DisconnectUnloadedObject(int connectionId);
        }

        private interface IpageAffichageAdherent_BindingsScopeConnector
        {
            global::System.WeakReference Parent { get; set; }
            bool ContainsElement(int connectionId);
            void RegisterForElementConnection(int connectionId, global::Microsoft.UI.Xaml.Markup.IComponentConnector connector);
        }

        internal global::Microsoft.UI.Xaml.Controls.GridView gv_adherent;
        internal global::Microsoft.UI.Xaml.Controls.TextBlock titre;
        internal global::Microsoft.UI.Xaml.Controls.TextBox tbx_nom;
        internal global::Microsoft.UI.Xaml.Controls.TextBlock nomErr;
        internal global::Microsoft.UI.Xaml.Controls.TextBox tbx_prenom;
        internal global::Microsoft.UI.Xaml.Controls.TextBlock prenomErr;
        internal global::Microsoft.UI.Xaml.Controls.TextBox tbx_adresse;
        internal global::Microsoft.UI.Xaml.Controls.TextBlock adresseErr;
        internal global::Microsoft.UI.Xaml.Controls.TextBox tbx_date_naissance;
        internal global::Microsoft.UI.Xaml.Controls.TextBlock date_naissanceErr;
        internal global::Microsoft.UI.Xaml.Controls.Button btn_modifier_bd;
#pragma warning restore 0649
#pragma warning restore 0169
    }
}

