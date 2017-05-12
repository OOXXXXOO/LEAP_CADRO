using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class FileLoad{

	private string enhance="31313513156451231";

	private static FileLoad instance = new FileLoad();

	private FileLoad(){
	}

	public static FileLoad getFileLoadInstance(){
		return instance;

	}

	public void setEnhance(string str){
		enhance = str;
	}
	public string getEnhance(){
		return enhance;
	}
}
