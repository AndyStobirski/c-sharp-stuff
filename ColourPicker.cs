 /*
     * Simple Colour Picker Combobox
     * 
     * This is a class derived from a combobox which contains
     * 141 standard colours and their names. When the drop down
     * is displayed the colour name with that that colour behind
     * it is displayed.
     * 
     */
    class ColourPicker : ComboBox
    {

        public ColourPicker()
        {
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            foreach (Color colour in new ColorConverter().GetStandardValues())
                if (!colour.IsSystemColor)
                    this.Items.Add(colour.Name);

           
            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        //get or set the controls selected colour
        public Color Color
        {
            get { return Color.FromName(this.SelectedText); }
            set
            {
                this.SelectedItem = value.Name.ToString();
            }
        }

        //override 
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index > -1)
            {
                
                // Paints the rectangle in colour taken from the list item name
                e.Graphics.FillRectangle(new SolidBrush(Color.FromName(this.Items[e.Index].ToString())), e.Bounds);
                Rectangle rec = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1);

                e.Graphics.DrawString(this.Items[e.Index].ToString(), new Font("Arial", 8f), new SolidBrush(Color.Black), rec);

                if ((e.State & DrawItemState.Focus) == 0)//out of focus
                {
                    // Draw the border rectangle
                    using (Pen pen = new Pen(Color.White))
                    {
                        e.Graphics.DrawRectangle(pen, rec);
                    }
                }
                else
                {
                    // Draw the border rectangle
                    using (Pen borderPen = new Pen(Color.White, 1))
                    {
                        e.Graphics.DrawRectangle(borderPen, rec);
                    }
                    // Draw the focus rectangle
                    Pen focusPen = new Pen(Color.Gray, 1);
                    focusPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    e.Graphics.DrawRectangle(focusPen, rec);
                    focusPen.Dispose();
                }
            }
        }    
    }
