using System;
using System.Drawing;
using System.Windows.Forms;

namespace Mmosoft.Oops.Layers
{
    // __NOTICE
    // The rule is: 
    // - Any external resources should be passed in child ctor and these resource 
    //   and DONOT release these resources in ReleaseResources method.
    // - Always release internal resource in ReleaseResources method.
    [Serializable]
    public class BaseDrawer
    {
        protected Control targetCtrl;

        public bool IsDesignMode { get; set; }
        public bool IsHovered { get; set; }

        public BaseDrawer(Control target)
        {
            this.targetCtrl = target;
        }

        /// <summary>
        /// Draw specified layer
        /// </summary>
        /// <param name="g">Graphic instance</param>
        /// <param name="r">Draw zone</param>
        /// <param name="inDesignMode">In design mode?</param>        
        public virtual void Draw(Graphics g, Rectangle r) { }
                
        public virtual void ReleaseResource() { }
    }
}