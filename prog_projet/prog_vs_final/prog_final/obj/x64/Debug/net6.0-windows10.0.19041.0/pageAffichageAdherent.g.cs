﻿#pragma checksum "E:\session_3_disc\prog_graph\prog_final\prog_projet\prog_vs_final\prog_final\pageAffichageAdherent.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4C5F0A68E1C1A696247894AB43BDB28835F6B928140F9645AB71DF5ED3A20225"
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
    partial class pageAffichageAdherent : 
        global::Microsoft.UI.Xaml.Controls.Page, 
        global::Microsoft.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2408")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private static class XamlBindingSetters
        {
            public static void Set_Microsoft_UI_Xaml_Documents_Run_Text(global::Microsoft.UI.Xaml.Documents.Run obj, global::System.String value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = targetNullValue;
                }
                obj.Text = value ?? global::System.String.Empty;
            }
        };

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2408")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private class pageAffichageAdherent_obj15_Bindings :
            global::Microsoft.UI.Xaml.IDataTemplateExtension,
            global::Microsoft.UI.Xaml.Markup.IDataTemplateComponent,
            global::Microsoft.UI.Xaml.Markup.IXamlBindScopeDiagnostics,
            global::Microsoft.UI.Xaml.Markup.IComponentConnector,
            IpageAffichageAdherent_Bindings
        {
            private global::prog_final.Adherent dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);
            private bool removedDataContextHandler = false;

            // Fields for each control that has bindings.
            private global::System.WeakReference obj15;
            private global::Microsoft.UI.Xaml.Documents.Run obj18;
            private global::Microsoft.UI.Xaml.Documents.Run obj19;
            private global::Microsoft.UI.Xaml.Documents.Run obj20;
            private global::Microsoft.UI.Xaml.Documents.Run obj21;
            private global::Microsoft.UI.Xaml.Documents.Run obj22;
            private global::Microsoft.UI.Xaml.Documents.Run obj23;

            // Static fields for each binding's enabled/disabled state
            private static bool isobj18TextDisabled = false;
            private static bool isobj19TextDisabled = false;
            private static bool isobj20TextDisabled = false;
            private static bool isobj21TextDisabled = false;
            private static bool isobj22TextDisabled = false;
            private static bool isobj23TextDisabled = false;

            public pageAffichageAdherent_obj15_Bindings()
            {
            }

            public void Disable(int lineNumber, int columnNumber)
            {
                if (lineNumber == 74 && columnNumber == 61)
                {
                    isobj18TextDisabled = true;
                }
                else if (lineNumber == 70 && columnNumber == 61)
                {
                    isobj19TextDisabled = true;
                }
                else if (lineNumber == 66 && columnNumber == 61)
                {
                    isobj20TextDisabled = true;
                }
                else if (lineNumber == 62 && columnNumber == 61)
                {
                    isobj21TextDisabled = true;
                }
                else if (lineNumber == 58 && columnNumber == 61)
                {
                    isobj22TextDisabled = true;
                }
                else if (lineNumber == 54 && columnNumber == 61)
                {
                    isobj23TextDisabled = true;
                }
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 15: // pageAffichageAdherent.xaml line 38
                        this.obj15 = new global::System.WeakReference(global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.StackPanel>(target));
                        break;
                    case 18: // pageAffichageAdherent.xaml line 74
                        this.obj18 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Documents.Run>(target);
                        break;
                    case 19: // pageAffichageAdherent.xaml line 70
                        this.obj19 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Documents.Run>(target);
                        break;
                    case 20: // pageAffichageAdherent.xaml line 66
                        this.obj20 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Documents.Run>(target);
                        break;
                    case 21: // pageAffichageAdherent.xaml line 62
                        this.obj21 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Documents.Run>(target);
                        break;
                    case 22: // pageAffichageAdherent.xaml line 58
                        this.obj22 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Documents.Run>(target);
                        break;
                    case 23: // pageAffichageAdherent.xaml line 54
                        this.obj23 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Documents.Run>(target);
                        break;
                    default:
                        break;
                }
            }
                        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2408")]
                        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                        public global::Microsoft.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target) 
                        {
                            return null;
                        }

            public void DataContextChangedHandler(global::Microsoft.UI.Xaml.FrameworkElement sender, global::Microsoft.UI.Xaml.DataContextChangedEventArgs args)
            {
                 if (this.SetDataRoot(args.NewValue))
                 {
                    this.Update();
                 }
            }

            // IDataTemplateExtension

            public bool ProcessBinding(uint phase)
            {
                throw new global::System.NotImplementedException();
            }

            public int ProcessBindings(global::Microsoft.UI.Xaml.Controls.ContainerContentChangingEventArgs args)
            {
                int nextPhase = -1;
                ProcessBindings(args.Item, args.ItemIndex, (int)args.Phase, out nextPhase);
                return nextPhase;
            }

            public void ResetTemplate()
            {
                Recycle();
            }

            // IDataTemplateComponent

            public void ProcessBindings(global::System.Object item, int itemIndex, int phase, out int nextPhase)
            {
                nextPhase = -1;
                switch(phase)
                {
                    case 0:
                        nextPhase = -1;
                        this.SetDataRoot(item);
                        if (!removedDataContextHandler)
                        {
                            removedDataContextHandler = true;
                            var rootElement = (this.obj15.Target as global::Microsoft.UI.Xaml.Controls.StackPanel);
                            if (rootElement != null)
                            {
                                rootElement.DataContextChanged -= this.DataContextChangedHandler;
                            }
                        }
                        this.initialized = true;
                        break;
                }
                this.Update_(global::WinRT.CastExtensions.As<global::prog_final.Adherent>(item), 1 << phase);
            }

            public void Recycle()
            {
            }

            // IpageAffichageAdherent_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
            }

            public void DisconnectUnloadedObject(int connectionId)
            {
                throw new global::System.ArgumentException("No unloadable elements to disconnect.");
            }

            public bool SetDataRoot(global::System.Object newDataRoot)
            {
                if (newDataRoot != null)
                {
                    this.dataRoot = global::WinRT.CastExtensions.As<global::prog_final.Adherent>(newDataRoot);
                    return true;
                }
                return false;
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::prog_final.Adherent obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_Age(obj.Age, phase);
                        this.Update_Date_naissance_string(obj.Date_naissance_string, phase);
                        this.Update_Adresse(obj.Adresse, phase);
                        this.Update_Nom(obj.Nom, phase);
                        this.Update_Prenom(obj.Prenom, phase);
                        this.Update_Matricule(obj.Matricule, phase);
                    }
                }
            }
            private void Update_Age(global::System.Int32 obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // pageAffichageAdherent.xaml line 74
                    if (!isobj18TextDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Documents_Run_Text(this.obj18, obj.ToString(), null);
                    }
                }
            }
            private void Update_Date_naissance_string(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // pageAffichageAdherent.xaml line 70
                    if (!isobj19TextDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Documents_Run_Text(this.obj19, obj, null);
                    }
                }
            }
            private void Update_Adresse(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // pageAffichageAdherent.xaml line 66
                    if (!isobj20TextDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Documents_Run_Text(this.obj20, obj, null);
                    }
                }
            }
            private void Update_Nom(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // pageAffichageAdherent.xaml line 62
                    if (!isobj21TextDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Documents_Run_Text(this.obj21, obj, null);
                    }
                }
            }
            private void Update_Prenom(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // pageAffichageAdherent.xaml line 58
                    if (!isobj22TextDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Documents_Run_Text(this.obj22, obj, null);
                    }
                }
            }
            private void Update_Matricule(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // pageAffichageAdherent.xaml line 54
                    if (!isobj23TextDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Documents_Run_Text(this.obj23, obj, null);
                    }
                }
            }
        }

        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2408")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // pageAffichageAdherent.xaml line 23
                {
                    this.message_reussite = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 3: // pageAffichageAdherent.xaml line 35
                {
                    this.gv_adherent = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.GridView>(target);
                    ((global::Microsoft.UI.Xaml.Controls.GridView)this.gv_adherent).SelectionChanged += this.gv_adherent_SelectionChanged;
                }
                break;
            case 4: // pageAffichageAdherent.xaml line 119
                {
                    this.titre = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 5: // pageAffichageAdherent.xaml line 213
                {
                    this.btn_modifier_bd = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)this.btn_modifier_bd).Click += this.btn_modifier_bd_Click;
                }
                break;
            case 6: // pageAffichageAdherent.xaml line 194
                {
                    this.datepicker_date_seance = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.DatePicker>(target);
                }
                break;
            case 7: // pageAffichageAdherent.xaml line 203
                {
                    this.datepicker_date_seanceErr = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 8: // pageAffichageAdherent.xaml line 173
                {
                    this.tbx_adresse = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBox>(target);
                }
                break;
            case 9: // pageAffichageAdherent.xaml line 183
                {
                    this.adresseErr = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 10: // pageAffichageAdherent.xaml line 152
                {
                    this.tbx_prenom = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBox>(target);
                }
                break;
            case 11: // pageAffichageAdherent.xaml line 162
                {
                    this.prenomErr = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 12: // pageAffichageAdherent.xaml line 131
                {
                    this.tbx_nom = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBox>(target);
                }
                break;
            case 13: // pageAffichageAdherent.xaml line 141
                {
                    this.nomErr = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 16: // pageAffichageAdherent.xaml line 78
                {
                    global::Microsoft.UI.Xaml.Controls.Button element16 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)element16).Click += this.btn_modifier_Click;
                }
                break;
            case 17: // pageAffichageAdherent.xaml line 89
                {
                    global::Microsoft.UI.Xaml.Controls.Button element17 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)element17).Click += this.btn_Supp_Click;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2408")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Microsoft.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Microsoft.UI.Xaml.Markup.IComponentConnector returnValue = null;
            switch(connectionId)
            {
            case 15: // pageAffichageAdherent.xaml line 38
                {                    
                    global::Microsoft.UI.Xaml.Controls.StackPanel element15 = (global::Microsoft.UI.Xaml.Controls.StackPanel)target;
                    pageAffichageAdherent_obj15_Bindings bindings = new pageAffichageAdherent_obj15_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(element15.DataContext);
                    element15.DataContextChanged += bindings.DataContextChangedHandler;
                    global::Microsoft.UI.Xaml.DataTemplate.SetExtensionInstance(element15, bindings);
                    global::Microsoft.UI.Xaml.Markup.XamlBindingHelper.SetDataTemplateComponent(element15, bindings);
                }
                break;
            }
            return returnValue;
        }
    }
}

