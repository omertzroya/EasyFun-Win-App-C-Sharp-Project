﻿

#pragma checksum "F:\Users\tomer\Documents\Visual Studio 2013\Projects\issieApp\mainApp\mainApp\balloons.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "9E5364D284F2E5588D7E2C6140C09114"
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
    partial class Balloons : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 16 "..\..\balloons.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.toHome_Tapped;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 19 "..\..\balloons.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.Mute_Tapped;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


