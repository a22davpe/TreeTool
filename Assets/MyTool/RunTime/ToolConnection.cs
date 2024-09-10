using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyTool
{
    [Serializable]
    public struct ToolConnection 
    {
        public ToolConnectionPort inputPort;
        public ToolConnectionPort outputPort;

        public ToolConnection(ToolConnectionPort inputPort, ToolConnectionPort outputPort)
        {
            this.inputPort = inputPort;
            this.outputPort = outputPort;
        }

        public ToolConnection(string inputPortId, int inputIndex, string outputPortId, int outputIndex)
        {
            inputPort = new ToolConnectionPort(inputPortId, inputIndex);
            outputPort = new ToolConnectionPort(outputPortId, outputIndex);
        }


    }

    [Serializable]
    public struct ToolConnectionPort
    {
        public string nodeId;
        public int portIndex;

        public ToolConnectionPort(string id, int index)
        {
            this.nodeId = id;
            this.portIndex = index;
        }
    }
}
