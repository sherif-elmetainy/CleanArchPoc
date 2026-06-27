namespace CodeArt.Poc.Core;

public record CommandResult(CommandResultStatus Status, string? Message);
public record CommandResult<T>(CommandResultStatus Status, T Data, string? Message) : CommandResult(Status, Message);
