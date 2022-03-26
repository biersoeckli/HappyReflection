# HappyReflection

[![nightly build](https://github.com/biersoeckli/HappyReflection/actions/workflows/dotnet-nightly.yml/badge.svg)](https://github.com/biersoeckli/HappyReflection/actions/workflows/dotnet-nightly.yml)

C# Helper util to easily find types by their name, interface or annotated attribute.

## Examples

### Find all classes that implement a specific interface
  
    // class declaration
    public class ChocolateFabric : IChocolateFabric {
      ...
    }
    
    // HappyReflection code      
    IList<Type> aListContainingChocolateFabric = HappyReflection.GetClassesByInterface<IChocolateFabric>();
    
    // result 
    new List<Type> { typeof(ChocolateFabric) }
    
    
### Find a type by it's name
  
    // class declaration
    public class ChocolateFabric {
      ...
    }
    
    // HappyReflection code    
    Type typeOfChocolateFabric = HappyReflection.GetTypeByName("ChocolateFabric");
    
    // result   
    typeof(ChocolateFabric)
    

### Find all classes annotating a specific attribute 
  
    // class declaration
    [ILoveChocolateAttribute]
    public class ChocolateFabric {
      ...
    }
    
    // HappyReflection code    
    IList<Type> typeOfChocolateFabric = HappyReflection.GetClassesByAttribute<ILoveChocolateAttribute>();
    
    // result   
    new List<Type> { typeof(ChocolateFabric) }
