﻿#pragma checksum "C:\école\projet_final_bd_prog\prog_projet\prog_vs_final\prog_final\pageStatistique3.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "7EB9799D2E331D2B6467D4168F7EA30193575ED0A55E6B66F3F189A1ECF1DC75"
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
    partial class pageStatistique3 : 
        global::Microsoft.UI.Xaml.Controls.Page, 
        global::Microsoft.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2408")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private static class XamlBindingSetters
        {
            public static void Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(global::Microsoft.UI.Xaml.Controls.TextBlock obj, global::System.String value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = targetNullValue;
                }
                obj.Text = value ?? global::System.String.Empty;
            }
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
        private class pageStatistique3_obj9_Bindings :
            global::Microsoft.UI.Xaml.IDataTemplateExtension,
            global::Microsoft.UI.Xaml.Markup.IDataTemplateComponent,
            global::Microsoft.UI.Xaml.Markup.IXamlBindScopeDiagnostics,
            global::Microsoft.UI.Xaml.Markup.IComponentConnector,
            IpageStatistique3_Bindings
        {
            private global::prog_final.Adherent dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);
            private bool removedDataContextHandler = false;

            // Fields for each control that has bindings.
            private global::System.WeakReference obj9;
            private global::Microsoft.UI.Xaml.Controls.TextBlock obj10;

            // Static fields for each binding's enabled/disabled state
            private static bool isobj10TextDisabled = false;

            public pageStatistique3_obj9_Bindings()
            {
            }

            public void Disable(int lineNumber, int columnNumber)
            {
                if (lineNumber == 167 && columnNumber == 44)
                {
                    isobj10TextDisabled = true;
                }
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 9: // pageStatistique3.xaml line 157
                        this.obj9 = new global::System.WeakReference(global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.StackPanel>(target));
                        break;
                    case 10: // pageStatistique3.xaml line 167
                        this.obj10 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
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
                            var rootElement = (this.obj9.Target as global::Microsoft.UI.Xaml.Controls.StackPanel);
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

            // IpageStatistique3_Bindings

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
                        this.Update_Nb_adherents(obj.Nb_adherents, phase);
                    }
                }
            }
            private void Update_Nb_adherents(global::System.Int32 obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // pageStatistique3.xaml line 167
                    if (!isobj10TextDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(this.obj10, obj.ToString(), null);
                    }
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2408")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private class pageStatistique3_obj12_Bindings :
            global::Microsoft.UI.Xaml.IDataTemplateExtension,
            global::Microsoft.UI.Xaml.Markup.IDataTemplateComponent,
            global::Microsoft.UI.Xaml.Markup.IXamlBindScopeDiagnostics,
            global::Microsoft.UI.Xaml.Markup.IComponentConnector,
            IpageStatistique3_Bindings
        {
            private global::prog_final.Activite dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);
            private bool removedDataContextHandler = false;

            // Fields for each control that has bindings.
            private global::System.WeakReference obj12;
            private global::Microsoft.UI.Xaml.Controls.TextBlock obj13;

            // Static fields for each binding's enabled/disabled state
            private static bool isobj13TextDisabled = false;

            public pageStatistique3_obj12_Bindings()
            {
            }

            public void Disable(int lineNumber, int columnNumber)
            {
                if (lineNumber == 135 && columnNumber == 44)
                {
                    isobj13TextDisabled = true;
                }
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 12: // pageStatistique3.xaml line 125
                        this.obj12 = new global::System.WeakReference(global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.StackPanel>(target));
                        break;
                    case 13: // pageStatistique3.xaml line 135
                        this.obj13 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
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
                            var rootElement = (this.obj12.Target as global::Microsoft.UI.Xaml.Controls.StackPanel);
                            if (rootElement != null)
                            {
                                rootElement.DataContextChanged -= this.DataContextChangedHandler;
                            }
                        }
                        this.initialized = true;
                        break;
                }
                this.Update_(global::WinRT.CastExtensions.As<global::prog_final.Activite>(item), 1 << phase);
            }

            public void Recycle()
            {
            }

            // IpageStatistique3_Bindings

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
                    this.dataRoot = global::WinRT.CastExtensions.As<global::prog_final.Activite>(newDataRoot);
                    return true;
                }
                return false;
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::prog_final.Activite obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_Nb_activites(obj.Nb_activites, phase);
                    }
                }
            }
            private void Update_Nb_activites(global::System.Int32 obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // pageStatistique3.xaml line 135
                    if (!isobj13TextDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(this.obj13, obj.ToString(), null);
                    }
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2408")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private class pageStatistique3_obj15_Bindings :
            global::Microsoft.UI.Xaml.IDataTemplateExtension,
            global::Microsoft.UI.Xaml.Markup.IDataTemplateComponent,
            global::Microsoft.UI.Xaml.Markup.IXamlBindScopeDiagnostics,
            global::Microsoft.UI.Xaml.Markup.IComponentConnector,
            IpageStatistique3_Bindings
        {
            private global::prog_final.Activite dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);
            private bool removedDataContextHandler = false;

            // Fields for each control that has bindings.
            private global::System.WeakReference obj15;
            private global::Microsoft.UI.Xaml.Documents.Run obj16;
            private global::Microsoft.UI.Xaml.Documents.Run obj17;

            // Static fields for each binding's enabled/disabled state
            private static bool isobj16TextDisabled = false;
            private static bool isobj17TextDisabled = false;

            public pageStatistique3_obj15_Bindings()
            {
            }

            public void Disable(int lineNumber, int columnNumber)
            {
                if (lineNumber == 103 && columnNumber == 61)
                {
                    isobj16TextDisabled = true;
                }
                else if (lineNumber == 97 && columnNumber == 61)
                {
                    isobj17TextDisabled = true;
                }
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 15: // pageStatistique3.xaml line 84
                        this.obj15 = new global::System.WeakReference(global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.StackPanel>(target));
                        break;
                    case 16: // pageStatistique3.xaml line 103
                        this.obj16 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Documents.Run>(target);
                        break;
                    case 17: // pageStatistique3.xaml line 97
                        this.obj17 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Documents.Run>(target);
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
                this.Update_(global::WinRT.CastExtensions.As<global::prog_final.Activite>(item), 1 << phase);
            }

            public void Recycle()
            {
            }

            // IpageStatistique3_Bindings

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
                    this.dataRoot = global::WinRT.CastExtensions.As<global::prog_final.Activite>(newDataRoot);
                    return true;
                }
                return false;
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::prog_final.Activite obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_Nbr_seance_par_activite(obj.Nbr_seance_par_activite, phase);
                        this.Update_Nom_activite(obj.Nom_activite, phase);
                    }
                }
            }
            private void Update_Nbr_seance_par_activite(global::System.Int32 obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // pageStatistique3.xaml line 103
                    if (!isobj16TextDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Documents_Run_Text(this.obj16, obj.ToString(), null);
                    }
                }
            }
            private void Update_Nom_activite(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // pageStatistique3.xaml line 97
                    if (!isobj17TextDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Documents_Run_Text(this.obj17, obj, null);
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
            case 2: // pageStatistique3.xaml line 71
                {
                    this.header_nbr_seance_par_activite = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 3: // pageStatistique3.xaml line 81
                {
                    this.gv_nbr_seance_par_activite = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.GridView>(target);
                    ((global::Microsoft.UI.Xaml.Controls.GridView)this.gv_nbr_seance_par_activite).SelectionChanged += this.gv_nbr_seance_par_activite_SelectionChanged;
                }
                break;
            case 4: // pageStatistique3.xaml line 112
                {
                    this.header_nb_activites = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 5: // pageStatistique3.xaml line 122
                {
                    this.gv_nb_activites = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.GridView>(target);
                    ((global::Microsoft.UI.Xaml.Controls.GridView)this.gv_nb_activites).SelectionChanged += this.gv_nb_activites_SelectionChanged;
                }
                break;
            case 6: // pageStatistique3.xaml line 144
                {
                    this.header_nb_total_adherent = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 7: // pageStatistique3.xaml line 154
                {
                    this.gv_nb_total_adherent = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.GridView>(target);
                    ((global::Microsoft.UI.Xaml.Controls.GridView)this.gv_nb_total_adherent).SelectionChanged += this.gv_nb_total_adherent_SelectionChanged;
                }
                break;
            case 18: // pageStatistique3.xaml line 40
                {
                    global::Microsoft.UI.Xaml.Controls.Button element18 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)element18).Click += this.btn_prev_click;
                }
                break;
            case 19: // pageStatistique3.xaml line 50
                {
                    global::Microsoft.UI.Xaml.Controls.Button element19 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)element19).Click += this.btn_next_click;
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
            case 9: // pageStatistique3.xaml line 157
                {                    
                    global::Microsoft.UI.Xaml.Controls.StackPanel element9 = (global::Microsoft.UI.Xaml.Controls.StackPanel)target;
                    pageStatistique3_obj9_Bindings bindings = new pageStatistique3_obj9_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(element9.DataContext);
                    element9.DataContextChanged += bindings.DataContextChangedHandler;
                    global::Microsoft.UI.Xaml.DataTemplate.SetExtensionInstance(element9, bindings);
                    global::Microsoft.UI.Xaml.Markup.XamlBindingHelper.SetDataTemplateComponent(element9, bindings);
                }
                break;
            case 12: // pageStatistique3.xaml line 125
                {                    
                    global::Microsoft.UI.Xaml.Controls.StackPanel element12 = (global::Microsoft.UI.Xaml.Controls.StackPanel)target;
                    pageStatistique3_obj12_Bindings bindings = new pageStatistique3_obj12_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(element12.DataContext);
                    element12.DataContextChanged += bindings.DataContextChangedHandler;
                    global::Microsoft.UI.Xaml.DataTemplate.SetExtensionInstance(element12, bindings);
                    global::Microsoft.UI.Xaml.Markup.XamlBindingHelper.SetDataTemplateComponent(element12, bindings);
                }
                break;
            case 15: // pageStatistique3.xaml line 84
                {                    
                    global::Microsoft.UI.Xaml.Controls.StackPanel element15 = (global::Microsoft.UI.Xaml.Controls.StackPanel)target;
                    pageStatistique3_obj15_Bindings bindings = new pageStatistique3_obj15_Bindings();
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

