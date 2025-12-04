# -------------------------------
# Advent of Code Monorepo Makefile
# -------------------------------

SCAFFOLDER=Tools/Scaffolder
RUNNER=Runner
TESTS=Tests

# Default year (modify as needed)
YEAR=2025

# -------------------------------
# Scaffold a new day
# Usage:
#   make scaffold year=2025 day=07
#   make scaffold day=03      (uses default YEAR)
# -------------------------------
scaffold:
	@echo "Scaffolding AoC $(year) Day $(day)..."
	@dotnet run --project $(SCAFFOLDER) -- $(year) $(day)

# -------------------------------
# Run the AoC solution for a given day
# Usage:
#   make run year=2025 day=07
#   make run day=03
# -------------------------------
run:
	@echo "Running AoC $(year) Day $(day)..."
	@dotnet run --project $(RUNNER) -- --year $(year) --day $(day)

# -------------------------------
# Run the latest day of the latest year
# Usage:
#   make latest
# -------------------------------
latest:
	@echo "Running latest AoC day..."
	@dotnet run --project $(RUNNER)

# -------------------------------
# Auto-detect next unsolved day
# Scaffold the next empty 'dayXX.txt'
# Usage:
#   make next year=2025
# -------------------------------
next:
	@echo "Finding next unsolved day for $(year)..."
	@bash -c '\
		dir="Years/AoC$(year)/input"; \
		mkdir -p $$dir; \
		for i in $$(printf "%02d " $$(seq 1 25)); do \
			echo "Checking file $$dir/day$$i.txt"; \
			if [ ! -f "$$dir/day$$i.txt" ]; then \
				echo "Scaffolding Day $$i"; \
				dotnet run --project $(SCAFFOLDER) -- $(year) $$i; \
				exit 0; \
			fi; \
		done; \
		echo "All 25 days already exist for year $(year)!" \
	'

# -------------------------------
# Run unit tests
# Usage:
#   make test
# -------------------------------
test:
	@echo "Running tests..."
	@dotnet test $(TESTS)

# -------------------------------
# Clean all build artifacts
# -------------------------------
clean:
	@echo "Cleaning solution..."
	@dotnet clean
	@find . -type d -name bin -exec rm -rf {} +
	@find . -type d -name obj -exec rm -rf {} +

.PHONY: scaffold run latest next test clean