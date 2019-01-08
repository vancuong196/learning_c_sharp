- We can add new operator with minimal change in the SupportedElement class and virtually don't need to re implement validator function. Because the validator does not depend on any operator.
 
-I am try to apply some design pattern to this project. You can see them at:
Singleton : ObjectLocatorService, LogService, FormulaCutter.
Factory Pattern: InformationService, GrammarValidator
Builder: FormulaElement
Observer: published: MainPageViewModel. observer: CalculatrTabModel, LogService.
Decorator : VariableGrammarValidator -DROP.
Strategy
