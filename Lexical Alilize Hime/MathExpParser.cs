/*
 * WARNING: this file has been generated by
 * Hime Parser Generator 3.4.0.0
 */
using System.Collections.Generic;
using Hime.Redist;
using Hime.Redist.Parsers;

namespace MathExp
{
	/// <summary>
	/// Represents a parser
	/// </summary>
	internal class MathExpParser : LRkParser
	{
		/// <summary>
		/// The automaton for this parser
		/// </summary>
		private static readonly LRkAutomaton commonAutomaton = LRkAutomaton.Find(typeof(MathExpParser), "MathExpParser.bin");
		/// <summary>
		/// Contains the constant IDs for the variables and virtuals in this parser
		/// </summary>
		public class ID
		{
			/// <summary>
			/// The unique identifier for variable exp_atom
			/// </summary>
			public const int VariableExpAtom = 0x0008;
			/// <summary>
			/// The unique identifier for variable exp
			/// </summary>
			public const int VariableExp = 0x0009;
		}
		/// <summary>
		/// The collection of variables matched by this parser
		/// </summary>
		/// <remarks>
		/// The variables are in an order consistent with the automaton,
		/// so that variable indices in the automaton can be used to retrieve the variables in this table
		/// </remarks>
		private static readonly Symbol[] variables = {
			new Symbol(0x0008, "exp_atom"), 
			new Symbol(0x0009, "exp"), 
			new Symbol(0x000A, "__VAxiom") };
		/// <summary>
		/// The collection of virtuals matched by this parser
		/// </summary>
		/// <remarks>
		/// The virtuals are in an order consistent with the automaton,
		/// so that virtual indices in the automaton can be used to retrieve the virtuals in this table
		/// </remarks>
		private static readonly Symbol[] virtuals = {
 };
		/// <summary>
		/// Initializes a new instance of the parser
		/// </summary>
		/// <param name="lexer">The input lexer</param>
		public MathExpParser(MathExpLexer lexer) : base (commonAutomaton, variables, virtuals, null, lexer) { }

		/// <summary>
		/// Visitor interface
		/// </summary>
		public class Visitor
		{
			public virtual void OnTerminalTrash(ASTNode node) {}
			public virtual void OnTerminalWhiteSpace(ASTNode node) {}
			public virtual void OnTerminalSeparator(ASTNode node) {}
			public virtual void OnTerminalSubgoal(ASTNode node) {}
			public virtual void OnTerminalGoal(ASTNode node) {}
			public virtual void OnVariableExpAtom(ASTNode node) {}
			public virtual void OnVariableExp(ASTNode node) {}
		}

		/// <summary>
		/// Walk the AST using a visitor
		/// </summary>
		public static void Visit(ParseResult result, Visitor visitor)
		{
			VisitASTNode(result.Root, visitor);
		}

		/// <summary>
		/// Walk the AST using a visitor
		/// </summary>
		public static void VisitASTNode(ASTNode node, Visitor visitor)
		{
			for (int i = 0; i < node.Children.Count; i++)
				VisitASTNode(node.Children[i], visitor);
			switch(node.Symbol.ID)
			{
				case 0x0003: visitor.OnTerminalTrash(node); break;
				case 0x0004: visitor.OnTerminalWhiteSpace(node); break;
				case 0x0005: visitor.OnTerminalSeparator(node); break;
				case 0x0006: visitor.OnTerminalSubgoal(node); break;
				case 0x0007: visitor.OnTerminalGoal(node); break;
				case 0x0008: visitor.OnVariableExpAtom(node); break;
				case 0x0009: visitor.OnVariableExp(node); break;
			}
		}
	}
}
