﻿#pragma checksum "E:\session_3_disc\prog_graph\prog_final\prog_projet\prog_vs_final\prog_final\pageModifierSeance.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "3ACC28DA6A976CF85F47D4F150AC8302977455B0D38345432361F9458015518C"
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
    partial class pageModifierSeance : 
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
            public static void Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(global::Microsoft.UI.Xaml.Controls.TextBlock obj, global::System.String value, string targetNullValue)
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
        private class pageModifierSeance_obj16_Bindings :
            global::Microsoft.UI.Xaml.IDataTemplateExtension,
            global::Microsoft.UI.Xaml.Markup.IDataTemplateComponent,
            global::Microsoft.UI.Xaml.Markup.IXamlBindScopeDiagnostics,
            global::Microsoft.UI.Xaml.Markup.IComponentConnector,
            IpageModifierSeance_Bindings
        {
            private global::prog_final.Seance dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);
            private bool removedDataContextHandler = false;

            // Fields for each control that has bindings.
            private global::System.WeakReference obj16;
            private global::Microsoft.UI.Xaml.Documents.Run obj19;
            private global::Microsoft.UI.Xaml.Documents.Run obj20;
            private global::Microsoft.UI.Xaml.Documents.Run obj21;
            private global::Microsoft.UI.Xaml.Documents.Run obj22;
            private global::Microsoft.UI.Xaml.Documents.Run obj23;
            private global::Microsoft.UI.Xaml.Controls.TextBlock obj24;

            // Static fields for each binding's enabled/disabled state
            private static bool isobj19TextDisabled = false;
            private static bool isobj20TextDisabled = false;
            private static bool isobj21TextDisabled = false;
            private static bool isobj22TextDisabled = false;
            private static bool isobj23TextDisabled = false;
            private static bool isobj24TextDisabled = false;

            public pageModifierSeance_obj16_Bindings()
            {
            }

            public void Disable(int lineNumber, int columnNumber)
            {
                if (lineNumber == 99 && columnNumber == 61)
                {
                    isobj19TextDisabled = true;
                }
                else if (lineNumber == 93 && columnNumber == 61)
                {
                    isobj20TextDisabled = true;
                }
                else if (lineNumber == 87 && columnNumber == 61)
                {
                    isobj21TextDisabled = true;
                }
                else if (lineNumber == 81 && columnNumber == 61)
                {
                    isobj22TextDisabled = true;
                }
                else if (lineNumber == 75 && columnNumber == 61)
                {
                    isobj23TextDisabled = true;
                }
                else if (lineNumber == 68 && columnNumber == 44)
                {
                    isobj24TextDisabled = true;
                }
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 16: // pageModifierSeance.xaml line 52
                        this.obj16 = new global::System.WeakReference(global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.StackPanel>(target));
                        break;
                    case 19: // pageModifierSeance.xaml line 99
                        this.obj19 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Documents.Run>(target);
                        break;
                    case 20: // pageModifierSeance.xaml line 93
                        this.obj20 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Documents.Run>(target);
                        break;
                    case 21: // pageModifierSeance.xaml line 87
                        this.obj21 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Documents.Run>(target);
                        break;
                    case 22: // pageModifierSeance.xaml line 81
                        this.obj22 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Documents.Run>(target);
                        break;
                    case 23: // pageModifierSeance.xaml line 75
                        this.obj23 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Documents.Run>(target);
                        break;
                    case 24: // pageModifierSeance.xaml line 68
                        this.obj24 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
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
                            var rootElement = (this.obj16.Target as global::Microsoft.UI.Xaml.Controls.StackPanel);
                            if (rootElement != null)
                            {
                                rootElement.DataContextChanged -= this.DataContextChangedHandler;
                            }
                        }
                        this.initialized = true;
                        break;
                }
                this.Update_(global::WinRT.CastExtensions.As<global::prog_final.Seance>(item), 1 << phase);
            }

            public void Recycle()
            {
            }

            // IpageModifierSeance_Bindings

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
                    this.dataRoot = global::WinRT.CastExtensions.As<global::prog_final.Seance>(newDataRoot);
                    return true;
                }
                return false;
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::prog_final.Seance obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_Moyenne_appreciation(obj.Moyenne_appreciation, phase);
                        this.Update_Nbr_place_disponible(obj.Nbr_place_disponible, phase);
                        this.Update_Nbr_inscription(obj.Nbr_inscription, phase);
                        this.Update_Heure(obj.Heure, phase);
                        this.Update_Date_seance_string(obj.Date_seance_string, phase);
                        this.Update_NomActivite(obj.NomActivite, phase);
                    }
                }
            }
            private void Update_Moyenne_appreciation(global::System.Double obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // pageModifierSeance.xaml line 99
                    if (!isobj19TextDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Documents_Run_Text(this.obj19, obj.ToString(), null);
                    }
                }
            }
            private void Update_Nbr_place_disponible(global::System.Int32 obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // pageModifierSeance.xaml line 93
                    if (!isobj20TextDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Documents_Run_Text(this.obj20, obj.ToString(), null);
                    }
                }
            }
            private void Update_Nbr_inscription(global::System.Int32 obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // pageModifierSeance.xaml line 87
                    if (!isobj21TextDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Documents_Run_Text(this.obj21, obj.ToString(), null);
                    }
                }
            }
            private void Update_Heure(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // pageModifierSeance.xaml line 81
                    if (!isobj22TextDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Documents_Run_Text(this.obj22, obj, null);
                    }
                }
            }
            private void Update_Date_seance_string(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // pageModifierSeance.xaml line 75
                    if (!isobj23TextDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Documents_Run_Text(this.obj23, obj, null);
                    }
                }
            }
            private void Update_NomActivite(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // pageModifierSeance.xaml line 68
                    if (!isobj24TextDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(this.obj24, obj, null);
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
            case 2: // pageModifierSeance.xaml line 27
                {
                    this.titre_gv = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 3: // pageModifierSeance.xaml line 37
                {
                    this.message_reussite = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 4: // pageModifierSeance.xaml line 49
                {
                    this.gv_seance = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.GridView>(target);
                    ((global::Microsoft.UI.Xaml.Controls.GridView)this.gv_seance).SelectionChanged += this.gv_seance_SelectionChanged;
                }
                break;
            case 5: // pageModifierSeance.xaml line 132
                {
                    this.titre = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 6: // pageModifierSeance.xaml line 225
                {
                    this.btn_modifier_BD = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)this.btn_modifier_BD).Click += this.btn_modifier_BD_Click;
                }
                break;
            case 7: // pageModifierSeance.xaml line 205
                {
                    this.tbx_nbrPlaceDispo = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBox>(target);
                }
                break;
            case 8: // pageModifierSeance.xaml line 215
                {
                    this.tbx_nbrPlaceDispoErr = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 9: // pageModifierSeance.xaml line 184
                {
                    this.tbx_heure = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBox>(target);
                }
                break;
            case 10: // pageModifierSeance.xaml line 194
                {
                    this.tbx_heureErr = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 11: // pageModifierSeance.xaml line 164
                {
                    this.datepicker_date_seance = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.DatePicker>(target);
                }
                break;
            case 12: // pageModifierSeance.xaml line 173
                {
                    this.datepicker_date_seanceErr = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 13: // pageModifierSeance.xaml line 144
                {
                    this.cbx_Activite = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ComboBox>(target);
                }
                break;
            case 14: // pageModifierSeance.xaml line 153
                {
                    this.cbx_ActiviteErr = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 17: // pageModifierSeance.xaml line 103
                {
                    global::Microsoft.UI.Xaml.Controls.Button element17 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)element17).Click += this.btn_modifier_Click;
                }
                break;
            case 18: // pageModifierSeance.xaml line 115
                {
                    global::Microsoft.UI.Xaml.Controls.Button element18 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)element18).Click += this.btn_Supp_Click;
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
            case 16: // pageModifierSeance.xaml line 52
                {                    
                    global::Microsoft.UI.Xaml.Controls.StackPanel element16 = (global::Microsoft.UI.Xaml.Controls.StackPanel)target;
                    pageModifierSeance_obj16_Bindings bindings = new pageModifierSeance_obj16_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(element16.DataContext);
                    element16.DataContextChanged += bindings.DataContextChangedHandler;
                    global::Microsoft.UI.Xaml.DataTemplate.SetExtensionInstance(element16, bindings);
                    global::Microsoft.UI.Xaml.Markup.XamlBindingHelper.SetDataTemplateComponent(element16, bindings);
                }
                break;
            }
            return returnValue;
        }
    }
}

