using System.Diagnostics.CodeAnalysis;
using System.Security;

namespace OsuPlayer.Modules.Hotkeys;

public struct WinMessage
{
    /// <SecurityNote>
    ///     Critical: Setting critical data
    /// </SecurityNote>
    [SecurityCritical]
    internal WinMessage(IntPtr hwnd, int message, IntPtr wParam, IntPtr lParam, int time, int pt_x, int pt_y)
    {
        _hwnd = hwnd;
        _message = message;
        _wParam = wParam;
        _lParam = lParam;
        _time = time;
        _pt_x = pt_x;
        _pt_y = pt_y;
    }
        
    //
    // Public Properties:
    //
        
    /// <summary> 
    ///     The handle of the window to which the message was sent. 
    /// </summary>
    /// <SecurityNote>
    ///     Critical: This data can not be modified by Partial Trust code as that may be exploited for spoofing purposes
    ///     PublicOK: This data is safe for Partial Trust code to read (getter), There is a demand on the setter to block Partial Trust code
    /// </SecurityNote>
    [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
    [SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible")]
    [SuppressMessage("Microsoft.Reliability", "CA2006:UseSafeHandleToEncapsulateNativeResources")]
    public IntPtr hwnd
    {
        [SecurityCritical]
        get
        {
            return _hwnd;
        }
        [SecurityCritical]
        set
        {
            _hwnd = value;   
        }
            
    }
        
    /// <summary> 
    ///    The Value of the window message. 
    /// </summary>
    /// <SecurityNote>
    ///     Critical: This data can not be modified by Partial Trust code as that may be exploited for spoofing purposes
    ///     PublicOK: This data is safe for Partial Trust code to read (getter), There is a demand on the setter to block Partial Trust code
    /// </SecurityNote>
    public int message
    {
        [SecurityCritical]
        get
        {
            return _message;
        }
        [SecurityCritical]
        set
        {
            _message = value;   
        }
            
    }
 
    /// <summary> 
    ///     The wParam of the window message. 
    /// </summary>
    /// <SecurityNote>
    ///     Critical: This data can not be modified by Partial Trust code as that may be exploited for spoofing purposes
    ///     PublicOK: This data is safe for Partial Trust code to read (getter), There is a demand on the setter to block Partial Trust code
    /// </SecurityNote>
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
    [SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible")]
    [SuppressMessage("Microsoft.Reliability", "CA2006:UseSafeHandleToEncapsulateNativeResources")]
    public IntPtr wParam
    {
        [SecurityCritical]
        get
        {
            return _wParam;
        }
        [SecurityCritical]
        set
        {
            _wParam = value;   
        }
            
    }
 
    /// <summary> 
    ///     The lParam of the window message. 
    /// </summary>
    /// <SecurityNote>
    ///     Critical: This data can not be modified by Partial Trust code as that may be exploited for spoofing purposes
    ///     PublicOK: This data is safe for Partial Trust code to read (getter), There is a demand on the setter to block Partial Trust code
    /// </SecurityNote>
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
    [SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible")]
    [SuppressMessage("Microsoft.Reliability", "CA2006:UseSafeHandleToEncapsulateNativeResources")]
    public IntPtr lParam
    {
        [SecurityCritical]
        get
        {
            return _lParam;
        }
        [SecurityCritical]
        set
        {
            _lParam = value;   
        }
            
    }
 
    /// <summary>
    ///     The time the window message was sent.
    /// </summary>
    /// <SecurityNote>
    ///     Critical: This data can not be modified by Partial Trust code as that may be exploited for spoofing purposes
    ///     PublicOK: This data is safe for Partial Trust code to read (getter), There is a demand on the setter to block Partial Trust code
    /// </SecurityNote>
    public int time
    {
        [SecurityCritical]
        get
        {
            return _time;
        }
        [SecurityCritical]
        set
        {
            _time = value;   
        }
            
    }
 
    // In the original Win32, pt was a by-Value POINT structure
    /// <summary> 
    ///     The X coordinate of the message POINT struct. 
    /// </summary>
    /// <SecurityNote>
    ///     Critical: This data can not be modified by Partial Trust code as that may be exploited for spoofing purposes
    ///     PublicOK: This data is safe for Partial Trust code to read (getter), There is a demand on the setter to block Partial Trust code
    /// </SecurityNote>
    [SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUpperCased")]
    [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
    public int pt_x
    {
        [SecurityCritical]
        get
        {
            return _pt_x;
        }
        [SecurityCritical]
        set
        {
            _pt_x = value;   
        }
            
    }
 
    /// <summary> 
    ///     The Y coordinate of the message POINT struct. 
    /// </summary>
    /// <SecurityNote>
    ///     Critical: This data can not be modified by Partial Trust code as that may be exploited for spoofing purposes
    ///     PublicOK: This data is safe for Partial Trust code to read (getter), There is a demand on the setter to block Partial Trust code
    /// </SecurityNote>
    [SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUpperCased")]
    [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
    public int pt_y
    {
        [SecurityCritical]
        get
        {
            return _pt_y;
        }
        [SecurityCritical]
        set
        {
            _pt_y = value;   
        }
            
    }
 
    //
    // Internal data:
    // - do not alter the number, order or size of ANY of this members!
    // - they must agree EXACTLY with the native Win32 MSG structure
 
    /// <summary>
    ///     The handle of the window to which the message was sent. 
    /// </summary>
    /// <SecurityNote>
    ///     Critical: This data can not be modified by Partial Trust code for spoofing purposes
    /// </SecurityNote>
    [SecurityCritical]
    private IntPtr _hwnd;
 
    /// <summary>
    ///     The Value of the window message. 
    /// </summary>
    /// <SecurityNote>
    ///     Critical: This data can not be modified by Partial Trust code for spoofing purposes
    /// </SecurityNote>
    [SecurityCritical]
    private int _message;
 
    /// <summary>
    ///     The wParam of the window message. 
    /// </summary>
    /// <SecurityNote>
    ///     Critical: This data can not be modified by Partial Trust code for spoofing purposes
    /// </SecurityNote>
    [SecurityCritical]
    private IntPtr _wParam;
 
    /// <summary>
    ///     The lParam of the window message. 
    /// </summary>
    /// <SecurityNote>
    ///     Critical: This data can not be modified by Partial Trust code for spoofing purposes
    /// </SecurityNote>
    [SecurityCritical]
    private IntPtr _lParam;
 
    /// <summary>
    ///     The time the window message was sent.
    /// </summary>
    /// <SecurityNote>
    ///     Critical: This data can not be modified by Partial Trust code for spoofing purposes
    /// </SecurityNote>
    [SecurityCritical]
    private int _time;
 
    /// <summary>
    ///     The X coordinate of the message POINT struct. 
    /// </summary>
    /// <SecurityNote>
    ///     Critical: This data can not be modified by Partial Trust code for spoofing purposes
    /// </SecurityNote>
    [SecurityCritical]
    private int _pt_x;
 
    /// <summary>
    ///     The Y coordinate of the message POINT struct. 
    /// </summary>
    /// <SecurityNote>
    ///     Critical: This data can not be modified by Partial Trust code for spoofing purposes
    /// </SecurityNote>
    [SecurityCritical]
    private int _pt_y;
 
}