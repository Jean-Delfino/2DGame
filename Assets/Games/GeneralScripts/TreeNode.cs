using System.Collections;
using System.Collections.Generic;

public class TreeNode{
    private List<TreeNode> children = new List<TreeNode>();
    string leafResult = null;
    string id;

    public TreeNode(){

    }

    TreeNode(string lR, List<TreeNode> nexts){
        children = new List<TreeNode>(nexts);
        leafResult = string.Copy(lR);
    }

    public string GetLeafResult(){
        return leafResult;
    }

    public void AddChild(TreeNode next){
        children.Add(next);
    }

    public TreeNode CompareID(string id){
        if(this.id == id)
            return this;

        return null;
    }

    public TreeNode CompareIDChild(int indexChild, string identifier, ref int error){
        if(indexChild > children.Count){
            error = 1;
            return null;
        }

        return children[indexChild].CompareID(identifier);
    }
}
