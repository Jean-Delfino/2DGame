using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NNodesTree{
    private List<TreeNode> nexts;

    TreeNode actualNode = null;
    NNodesTree(List<TreeNode> nextsC){
        nexts = new List<TreeNode>(nextsC);
    }

    public void SearchNode(string id){
        TreeNode nodeSearch = new TreeNode();
        int error = 0;
        int i = 0;

        if(actualNode == null){
            SearchChildren(id);
            return;
        }

        while(nodeSearch == null && error == 0){
            nodeSearch = actualNode.CompareIDChild(i , id, ref error);
            i++;
        }

        actualNode = nodeSearch;
    }

    void SearchChildren(string id){
        int i = 0;
        while(actualNode == null && i < nexts.Count){
            actualNode = nexts[i].CompareID(id);
            i++;
        }
    }

    public bool CompareActualNode(string desired){
        if(desired == actualNode.GetLeafResult()){
            return true;
        }
        return false;
    }
}
