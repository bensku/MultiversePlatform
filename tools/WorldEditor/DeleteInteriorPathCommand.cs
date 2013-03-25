/********************************************************************

The Multiverse Platform is made available under the MIT License.

Copyright (c) 2012 The Multiverse Foundation

Permission is hereby granted, free of charge, to any person 
obtaining a copy of this software and associated documentation 
files (the "Software"), to deal in the Software without restriction, 
including without limitation the rights to use, copy, modify, 
merge, publish, distribute, sublicense, and/or sell copies 
of the Software, and to permit persons to whom the Software 
is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be 
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, 
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES 
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND 
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE 
OR OTHER DEALINGS IN THE SOFTWARE.

*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Multiverse.Tools.WorldEditor
{
    public class DeleteInteriorPathCommandFactory : ICommandFactory
    {
        WorldEditor app;
        IWorldContainer parent;
        InteriorPath path;

        public DeleteInteriorPathCommandFactory(WorldEditor worldEditor, IWorldContainer parentObject, InteriorPath path)
        {
            app = worldEditor;
            parent = parentObject;
            this.path = path; 
        }

        #region ICommandFactory Members

        public ICommand CreateCommand()
        {
            ICommand cmd = new DeleteInteriorPathCommand(app, parent, path);

            return cmd;
        }

        #endregion
    }

    public class DeleteInteriorPathCommand : ICommand
    {
        #region ICommand Members

        private WorldEditor app;
        private IWorldContainer parent;
        private InteriorPath path;

        public DeleteInteriorPathCommand(WorldEditor worldEditor, IWorldContainer parentObject, InteriorPath path)
        {
            this.app = worldEditor;
            this.parent = parentObject;
			this.path = path;
        }

        public bool Undoable()
        {
            return true;
        }

        public void Execute()
        {
			parent.Remove(path);
			((StaticObject)parent).PathsPerInstance = true;
        }

        public void UnExecute()
        {
            parent.Add(path);
			((StaticObject)parent).PathsPerInstance = true;
        }

        #endregion

    }

}