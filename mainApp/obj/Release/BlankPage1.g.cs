﻿

#pragma checksum "F:\Users\tomer\Documents\Visual Studio 2013\Projects\issieApp\mainApp\mainApp\BlankPage1.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "42E045174AAB50ADC0709DBEA72C87AF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace mainApp
{
    partial class ProfilesPage : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 37 "..\..\BlankPage1.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.AddProfile;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 38 "..\..\BlankPage1.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.cencel_tapped;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 18 "..\..\BlankPage1.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.ScrollLeftGrid_OnTapped;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 21 "..\..\BlankPage1.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.ScrollRightGrid_OnTapped;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


