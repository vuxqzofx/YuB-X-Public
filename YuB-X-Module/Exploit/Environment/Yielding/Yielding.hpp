#pragma once

#include <functional>
#include <memory>
#include <lua.h>
#include <thread>

using Yielded = std::function<int(lua_State*)>;
namespace Yielding
{
	int YieldExecution(lua_State* L, const std::function<Yielded()>& Function);
	void RunYield();
}