#define ICALL_TABLE_corlib 1

static int corlib_icall_indexes [] = {
226,
238,
239,
240,
241,
242,
243,
244,
245,
246,
249,
250,
251,
420,
421,
422,
451,
452,
453,
473,
474,
475,
476,
593,
594,
595,
598,
642,
643,
644,
647,
649,
651,
653,
658,
666,
667,
668,
669,
670,
671,
672,
673,
674,
675,
676,
677,
678,
679,
680,
681,
682,
684,
685,
686,
687,
688,
689,
690,
787,
788,
789,
790,
791,
792,
793,
794,
795,
796,
797,
798,
799,
800,
801,
802,
803,
805,
806,
807,
808,
809,
810,
811,
878,
879,
948,
955,
958,
960,
966,
967,
969,
970,
974,
976,
979,
980,
982,
984,
985,
988,
989,
990,
993,
995,
998,
1000,
1002,
1009,
1014,
1088,
1090,
1092,
1102,
1103,
1104,
1105,
1107,
1114,
1115,
1116,
1117,
1118,
1126,
1127,
1128,
1132,
1133,
1136,
1140,
1141,
1142,
1426,
1645,
1646,
9738,
9739,
9741,
9742,
9743,
9744,
9745,
9746,
9748,
9750,
9752,
9753,
9754,
9765,
9767,
9772,
9774,
9776,
9778,
9828,
9834,
9835,
9837,
9838,
9839,
9840,
9841,
9843,
9845,
10954,
10958,
10960,
10961,
10962,
10963,
11230,
11231,
11232,
11233,
11254,
11255,
11256,
11258,
11260,
11312,
11392,
11394,
11396,
11406,
11407,
11408,
11409,
11410,
11874,
11875,
11880,
11881,
11920,
11956,
11963,
11970,
11981,
11985,
12009,
12092,
12094,
12105,
12107,
12108,
12109,
12116,
12131,
12151,
12152,
12160,
12162,
12169,
12170,
12173,
12175,
12180,
12186,
12187,
12194,
12196,
12208,
12211,
12212,
12213,
12224,
12233,
12239,
12240,
12241,
12243,
12244,
12261,
12263,
12277,
12300,
12301,
12302,
12329,
12359,
12360,
13019,
13033,
13127,
13128,
13348,
13349,
13357,
13358,
13359,
13365,
13464,
13963,
13964,
14373,
14374,
14379,
14389,
15771,
15792,
15794,
15796,
};
void ves_icall_System_Array_InternalCreate (int,int,int,int,int);
int ves_icall_System_Array_GetCorElementTypeOfElementTypeInternal (int);
int ves_icall_System_Array_IsValueOfElementTypeInternal (int,int);
int ves_icall_System_Array_CanChangePrimitive (int,int,int);
int ves_icall_System_Array_FastCopy (int,int,int,int,int);
int ves_icall_System_Array_GetLengthInternal_raw (int,int,int);
int ves_icall_System_Array_GetLowerBoundInternal_raw (int,int,int);
void ves_icall_System_Array_GetGenericValue_icall (int,int,int);
void ves_icall_System_Array_GetValueImpl_raw (int,int,int,int);
void ves_icall_System_Array_SetGenericValue_icall (int,int,int);
void ves_icall_System_Array_SetValueImpl_raw (int,int,int,int);
void ves_icall_System_Array_InitializeInternal_raw (int,int);
void ves_icall_System_Array_SetValueRelaxedImpl_raw (int,int,int,int);
void ves_icall_System_Runtime_RuntimeImports_ZeroMemory (int,int);
void ves_icall_System_Runtime_RuntimeImports_Memmove (int,int,int);
void ves_icall_System_Buffer_BulkMoveWithWriteBarrier (int,int,int,int);
int ves_icall_System_Delegate_AllocDelegateLike_internal_raw (int,int);
int ves_icall_System_Delegate_CreateDelegate_internal_raw (int,int,int,int,int);
int ves_icall_System_Delegate_GetVirtualMethod_internal_raw (int,int);
void ves_icall_System_Enum_GetEnumValuesAndNames_raw (int,int,int,int);
void ves_icall_System_Enum_InternalBoxEnum_raw (int,int,int64_t,int);
int ves_icall_System_Enum_InternalGetCorElementType (int);
void ves_icall_System_Enum_InternalGetUnderlyingType_raw (int,int,int);
int ves_icall_System_Environment_get_ProcessorCount ();
int ves_icall_System_Environment_get_TickCount ();
int64_t ves_icall_System_Environment_get_TickCount64 ();
void ves_icall_System_Environment_FailFast_raw (int,int,int,int);
int ves_icall_System_GC_GetCollectionCount (int);
void ves_icall_System_GC_register_ephemeron_array_raw (int,int);
int ves_icall_System_GC_get_ephemeron_tombstone_raw (int);
void ves_icall_System_GC_SuppressFinalize_raw (int,int);
void ves_icall_System_GC_ReRegisterForFinalize_raw (int,int);
void ves_icall_System_GC_GetGCMemoryInfo (int,int,int,int,int,int);
int ves_icall_System_GC_AllocPinnedArray_raw (int,int,int);
int ves_icall_System_Object_MemberwiseClone_raw (int,int);
double ves_icall_System_Math_Acos (double);
double ves_icall_System_Math_Acosh (double);
double ves_icall_System_Math_Asin (double);
double ves_icall_System_Math_Asinh (double);
double ves_icall_System_Math_Atan (double);
double ves_icall_System_Math_Atan2 (double,double);
double ves_icall_System_Math_Atanh (double);
double ves_icall_System_Math_Cbrt (double);
double ves_icall_System_Math_Ceiling (double);
double ves_icall_System_Math_Cos (double);
double ves_icall_System_Math_Cosh (double);
double ves_icall_System_Math_Exp (double);
double ves_icall_System_Math_Floor (double);
double ves_icall_System_Math_Log (double);
double ves_icall_System_Math_Log10 (double);
double ves_icall_System_Math_Pow (double,double);
double ves_icall_System_Math_Sin (double);
double ves_icall_System_Math_Sinh (double);
double ves_icall_System_Math_Sqrt (double);
double ves_icall_System_Math_Tan (double);
double ves_icall_System_Math_Tanh (double);
double ves_icall_System_Math_FusedMultiplyAdd (double,double,double);
double ves_icall_System_Math_Log2 (double);
double ves_icall_System_Math_ModF (double,int);
float ves_icall_System_MathF_Acos (float);
float ves_icall_System_MathF_Acosh (float);
float ves_icall_System_MathF_Asin (float);
float ves_icall_System_MathF_Asinh (float);
float ves_icall_System_MathF_Atan (float);
float ves_icall_System_MathF_Atan2 (float,float);
float ves_icall_System_MathF_Atanh (float);
float ves_icall_System_MathF_Cbrt (float);
float ves_icall_System_MathF_Ceiling (float);
float ves_icall_System_MathF_Cos (float);
float ves_icall_System_MathF_Cosh (float);
float ves_icall_System_MathF_Exp (float);
float ves_icall_System_MathF_Floor (float);
float ves_icall_System_MathF_Log (float);
float ves_icall_System_MathF_Log10 (float);
float ves_icall_System_MathF_Pow (float,float);
float ves_icall_System_MathF_Sin (float);
float ves_icall_System_MathF_Sinh (float);
float ves_icall_System_MathF_Sqrt (float);
float ves_icall_System_MathF_Tan (float);
float ves_icall_System_MathF_Tanh (float);
float ves_icall_System_MathF_FusedMultiplyAdd (float,float,float);
float ves_icall_System_MathF_Log2 (float);
float ves_icall_System_MathF_ModF (float,int);
void ves_icall_RuntimeMethodHandle_ReboxFromNullable_raw (int,int,int);
void ves_icall_RuntimeMethodHandle_ReboxToNullable_raw (int,int,int,int);
int ves_icall_RuntimeType_GetCorrespondingInflatedMethod_raw (int,int,int);
void ves_icall_RuntimeType_make_array_type_raw (int,int,int,int);
void ves_icall_RuntimeType_make_byref_type_raw (int,int,int);
void ves_icall_RuntimeType_make_pointer_type_raw (int,int,int);
void ves_icall_RuntimeType_MakeGenericType_raw (int,int,int,int);
int ves_icall_RuntimeType_GetMethodsByName_native_raw (int,int,int,int,int);
int ves_icall_RuntimeType_GetPropertiesByName_native_raw (int,int,int,int,int);
int ves_icall_RuntimeType_GetConstructors_native_raw (int,int,int);
void ves_icall_RuntimeType_GetInterfaceMapData_raw (int,int,int,int,int);
void ves_icall_RuntimeType_GetPacking_raw (int,int,int,int);
int ves_icall_System_RuntimeType_CreateInstanceInternal_raw (int,int);
void ves_icall_System_RuntimeType_AllocateValueType_raw (int,int,int,int);
void ves_icall_RuntimeType_GetDeclaringMethod_raw (int,int,int);
void ves_icall_System_RuntimeType_getFullName_raw (int,int,int,int,int);
void ves_icall_RuntimeType_GetGenericArgumentsInternal_raw (int,int,int,int);
int ves_icall_RuntimeType_GetGenericParameterPosition (int);
int ves_icall_RuntimeType_GetEvents_native_raw (int,int,int,int);
int ves_icall_RuntimeType_GetFields_native_raw (int,int,int,int,int);
void ves_icall_RuntimeType_GetInterfaces_raw (int,int,int);
int ves_icall_RuntimeType_GetNestedTypes_native_raw (int,int,int,int,int);
void ves_icall_RuntimeType_GetDeclaringType_raw (int,int,int);
void ves_icall_RuntimeType_GetName_raw (int,int,int);
void ves_icall_RuntimeType_GetNamespace_raw (int,int,int);
int ves_icall_RuntimeType_IsUnmanagedFunctionPointerInternal (int);
int ves_icall_RuntimeType_FunctionPointerReturnAndParameterTypes_raw (int,int);
int ves_icall_RuntimeTypeHandle_GetAttributes (int);
int ves_icall_RuntimeTypeHandle_GetMetadataToken_raw (int,int);
void ves_icall_RuntimeTypeHandle_GetGenericTypeDefinition_impl_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_GetCorElementType (int);
int ves_icall_RuntimeTypeHandle_HasInstantiation (int);
int ves_icall_RuntimeTypeHandle_IsComObject_raw (int,int);
int ves_icall_RuntimeTypeHandle_IsInstanceOfType_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_HasReferences_raw (int,int);
int ves_icall_RuntimeTypeHandle_GetArrayRank_raw (int,int);
void ves_icall_RuntimeTypeHandle_GetAssembly_raw (int,int,int);
void ves_icall_RuntimeTypeHandle_GetElementType_raw (int,int,int);
void ves_icall_RuntimeTypeHandle_GetModule_raw (int,int,int);
void ves_icall_RuntimeTypeHandle_GetBaseType_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_type_is_assignable_from_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_IsGenericTypeDefinition (int);
int ves_icall_RuntimeTypeHandle_GetGenericParameterInfo_raw (int,int);
int ves_icall_RuntimeTypeHandle_is_subclass_of_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_IsByRefLike_raw (int,int);
void ves_icall_System_RuntimeTypeHandle_internal_from_name_raw (int,int,int,int,int,int);
int ves_icall_System_String_FastAllocateString_raw (int,int);
int ves_icall_System_String_InternalIsInterned_raw (int,int);
int ves_icall_System_String_InternalIntern_raw (int,int);
int ves_icall_System_Type_internal_from_handle_raw (int,int);
int ves_icall_System_ValueType_InternalGetHashCode_raw (int,int,int);
int ves_icall_System_ValueType_Equals_raw (int,int,int,int);
int ves_icall_System_Threading_Interlocked_CompareExchange_Int (int,int,int);
void ves_icall_System_Threading_Interlocked_CompareExchange_Object (int,int,int,int);
int ves_icall_System_Threading_Interlocked_Decrement_Int (int);
int64_t ves_icall_System_Threading_Interlocked_Decrement_Long (int);
int ves_icall_System_Threading_Interlocked_Increment_Int (int);
int64_t ves_icall_System_Threading_Interlocked_Increment_Long (int);
int ves_icall_System_Threading_Interlocked_Exchange_Int (int,int);
void ves_icall_System_Threading_Interlocked_Exchange_Object (int,int,int);
int64_t ves_icall_System_Threading_Interlocked_CompareExchange_Long (int,int64_t,int64_t);
int64_t ves_icall_System_Threading_Interlocked_Exchange_Long (int,int64_t);
int64_t ves_icall_System_Threading_Interlocked_Read_Long (int);
int ves_icall_System_Threading_Interlocked_Add_Int (int,int);
int64_t ves_icall_System_Threading_Interlocked_Add_Long (int,int64_t);
void ves_icall_System_Threading_Monitor_Monitor_Enter_raw (int,int);
void mono_monitor_exit_icall_raw (int,int);
void ves_icall_System_Threading_Monitor_Monitor_pulse_raw (int,int);
void ves_icall_System_Threading_Monitor_Monitor_pulse_all_raw (int,int);
int ves_icall_System_Threading_Monitor_Monitor_wait_raw (int,int,int,int);
void ves_icall_System_Threading_Monitor_Monitor_try_enter_with_atomic_var_raw (int,int,int,int,int);
void ves_icall_System_Threading_Thread_StartInternal_raw (int,int,int);
void ves_icall_System_Threading_Thread_InitInternal_raw (int,int);
int ves_icall_System_Threading_Thread_GetCurrentThread ();
void ves_icall_System_Threading_InternalThread_Thread_free_internal_raw (int,int);
int ves_icall_System_Threading_Thread_GetState_raw (int,int);
void ves_icall_System_Threading_Thread_SetState_raw (int,int,int);
void ves_icall_System_Threading_Thread_ClrState_raw (int,int,int);
void ves_icall_System_Threading_Thread_SetName_icall_raw (int,int,int,int);
int ves_icall_System_Threading_Thread_YieldInternal ();
void ves_icall_System_Threading_Thread_SetPriority_raw (int,int,int);
void ves_icall_System_Runtime_Loader_AssemblyLoadContext_PrepareForAssemblyLoadContextRelease_raw (int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_GetLoadContextForAssembly_raw (int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFile_raw (int,int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalInitializeNativeALC_raw (int,int,int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFromStream_raw (int,int,int,int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalGetLoadedAssemblies_raw (int);
int ves_icall_System_GCHandle_InternalAlloc_raw (int,int,int);
void ves_icall_System_GCHandle_InternalFree_raw (int,int);
int ves_icall_System_GCHandle_InternalGet_raw (int,int);
void ves_icall_System_GCHandle_InternalSet_raw (int,int,int);
int ves_icall_System_Runtime_InteropServices_Marshal_GetLastPInvokeError ();
void ves_icall_System_Runtime_InteropServices_Marshal_SetLastPInvokeError (int);
void ves_icall_System_Runtime_InteropServices_Marshal_StructureToPtr_raw (int,int,int,int);
void ves_icall_System_Runtime_InteropServices_Marshal_PtrToStructureInternal_raw (int,int,int,int);
int ves_icall_System_Runtime_InteropServices_Marshal_SizeOfHelper_raw (int,int,int);
int ves_icall_System_Runtime_InteropServices_NativeLibrary_LoadByName_raw (int,int,int,int,int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalGetHashCode_raw (int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalTryGetHashCode_raw (int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetObjectValue_raw (int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetUninitializedObjectInternal_raw (int,int);
void ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InitializeArray_raw (int,int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetSpanDataFrom_raw (int,int,int,int);
void ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_RunClassConstructor_raw (int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_SufficientExecutionStack ();
int ves_icall_System_Reflection_Assembly_GetExecutingAssembly_raw (int,int);
int ves_icall_System_Reflection_Assembly_GetEntryAssembly_raw (int);
int ves_icall_System_Reflection_Assembly_InternalLoad_raw (int,int,int,int);
int ves_icall_System_Reflection_Assembly_InternalGetType_raw (int,int,int,int,int,int);
int ves_icall_System_Reflection_AssemblyName_GetNativeName (int);
int ves_icall_MonoCustomAttrs_GetCustomAttributesInternal_raw (int,int,int,int);
int ves_icall_MonoCustomAttrs_GetCustomAttributesDataInternal_raw (int,int);
int ves_icall_MonoCustomAttrs_IsDefinedInternal_raw (int,int,int);
int ves_icall_System_Reflection_FieldInfo_internal_from_handle_type_raw (int,int,int);
int ves_icall_System_Reflection_FieldInfo_get_marshal_info_raw (int,int);
int ves_icall_System_Reflection_LoaderAllocatorScout_Destroy (int);
void ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceNames_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeAssembly_GetExportedTypes_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeAssembly_GetInfo_raw (int,int,int,int);
int ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceInternal_raw (int,int,int,int,int);
void ves_icall_System_Reflection_Assembly_GetManifestModuleInternal_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeAssembly_GetModulesInternal_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeCustomAttributeData_ResolveArgumentsInternal_raw (int,int,int,int,int,int,int);
void ves_icall_RuntimeEventInfo_get_event_info_raw (int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_System_Reflection_EventInfo_internal_from_handle_type_raw (int,int,int);
int ves_icall_RuntimeFieldInfo_ResolveType_raw (int,int);
int ves_icall_RuntimeFieldInfo_GetParentType_raw (int,int,int);
int ves_icall_RuntimeFieldInfo_GetFieldOffset_raw (int,int);
int ves_icall_RuntimeFieldInfo_GetValueInternal_raw (int,int,int);
void ves_icall_RuntimeFieldInfo_SetValueInternal_raw (int,int,int,int);
int ves_icall_RuntimeFieldInfo_GetRawConstantValue_raw (int,int);
int ves_icall_reflection_get_token_raw (int,int);
void ves_icall_get_method_info_raw (int,int,int);
int ves_icall_get_method_attributes (int);
int ves_icall_System_Reflection_MonoMethodInfo_get_parameter_info_raw (int,int,int);
int ves_icall_System_MonoMethodInfo_get_retval_marshal_raw (int,int);
int ves_icall_System_Reflection_RuntimeMethodInfo_GetMethodFromHandleInternalType_native_raw (int,int,int,int);
int ves_icall_RuntimeMethodInfo_get_name_raw (int,int);
int ves_icall_RuntimeMethodInfo_get_base_method_raw (int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_InternalInvoke_raw (int,int,int,int,int);
void ves_icall_RuntimeMethodInfo_GetPInvoke_raw (int,int,int,int,int);
int ves_icall_RuntimeMethodInfo_MakeGenericMethod_impl_raw (int,int,int);
int ves_icall_RuntimeMethodInfo_GetGenericArguments_raw (int,int);
int ves_icall_RuntimeMethodInfo_GetGenericMethodDefinition_raw (int,int);
int ves_icall_RuntimeMethodInfo_get_IsGenericMethodDefinition_raw (int,int);
int ves_icall_RuntimeMethodInfo_get_IsGenericMethod_raw (int,int);
void ves_icall_InvokeClassConstructor_raw (int,int);
int ves_icall_InternalInvoke_raw (int,int,int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_System_Reflection_RuntimeModule_InternalGetTypes_raw (int,int);
void ves_icall_System_Reflection_RuntimeModule_GetGuidInternal_raw (int,int,int);
int ves_icall_System_Reflection_RuntimeModule_ResolveMethodToken_raw (int,int,int,int,int,int);
void ves_icall_RuntimePropertyInfo_get_property_info_raw (int,int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_System_Reflection_RuntimePropertyInfo_internal_from_handle_type_raw (int,int,int);
int ves_icall_CustomAttributeBuilder_GetBlob_raw (int,int,int,int,int,int,int,int);
void ves_icall_DynamicMethod_create_dynamic_method_raw (int,int,int,int,int);
void ves_icall_AssemblyBuilder_basic_init_raw (int,int);
void ves_icall_AssemblyBuilder_UpdateNativeCustomAttributes_raw (int,int);
void ves_icall_ModuleBuilder_basic_init_raw (int,int);
void ves_icall_ModuleBuilder_set_wrappers_type_raw (int,int,int);
int ves_icall_ModuleBuilder_getUSIndex_raw (int,int,int);
int ves_icall_ModuleBuilder_getToken_raw (int,int,int,int);
int ves_icall_ModuleBuilder_getMethodToken_raw (int,int,int,int);
void ves_icall_ModuleBuilder_RegisterToken_raw (int,int,int,int);
int ves_icall_TypeBuilder_create_runtime_class_raw (int,int);
int ves_icall_System_IO_Stream_HasOverriddenBeginEndRead_raw (int,int);
int ves_icall_System_IO_Stream_HasOverriddenBeginEndWrite_raw (int,int);
int ves_icall_System_Diagnostics_Debugger_IsAttached_internal ();
void ves_icall_System_Diagnostics_Debugger_Log (int,int,int);
int ves_icall_System_Diagnostics_StackFrame_GetFrameInfo (int,int,int,int,int,int,int,int);
void ves_icall_System_Diagnostics_StackTrace_GetTrace (int,int,int,int);
int ves_icall_Mono_RuntimeClassHandle_GetTypeFromClass (int);
void ves_icall_Mono_RuntimeGPtrArrayHandle_GPtrArrayFree (int);
int ves_icall_Mono_SafeStringMarshal_StringToUtf8 (int);
void ves_icall_Mono_SafeStringMarshal_GFree (int);
static void *corlib_icall_funcs [] = {
// token 226,
ves_icall_System_Array_InternalCreate,
// token 238,
ves_icall_System_Array_GetCorElementTypeOfElementTypeInternal,
// token 239,
ves_icall_System_Array_IsValueOfElementTypeInternal,
// token 240,
ves_icall_System_Array_CanChangePrimitive,
// token 241,
ves_icall_System_Array_FastCopy,
// token 242,
ves_icall_System_Array_GetLengthInternal_raw,
// token 243,
ves_icall_System_Array_GetLowerBoundInternal_raw,
// token 244,
ves_icall_System_Array_GetGenericValue_icall,
// token 245,
ves_icall_System_Array_GetValueImpl_raw,
// token 246,
ves_icall_System_Array_SetGenericValue_icall,
// token 249,
ves_icall_System_Array_SetValueImpl_raw,
// token 250,
ves_icall_System_Array_InitializeInternal_raw,
// token 251,
ves_icall_System_Array_SetValueRelaxedImpl_raw,
// token 420,
ves_icall_System_Runtime_RuntimeImports_ZeroMemory,
// token 421,
ves_icall_System_Runtime_RuntimeImports_Memmove,
// token 422,
ves_icall_System_Buffer_BulkMoveWithWriteBarrier,
// token 451,
ves_icall_System_Delegate_AllocDelegateLike_internal_raw,
// token 452,
ves_icall_System_Delegate_CreateDelegate_internal_raw,
// token 453,
ves_icall_System_Delegate_GetVirtualMethod_internal_raw,
// token 473,
ves_icall_System_Enum_GetEnumValuesAndNames_raw,
// token 474,
ves_icall_System_Enum_InternalBoxEnum_raw,
// token 475,
ves_icall_System_Enum_InternalGetCorElementType,
// token 476,
ves_icall_System_Enum_InternalGetUnderlyingType_raw,
// token 593,
ves_icall_System_Environment_get_ProcessorCount,
// token 594,
ves_icall_System_Environment_get_TickCount,
// token 595,
ves_icall_System_Environment_get_TickCount64,
// token 598,
ves_icall_System_Environment_FailFast_raw,
// token 642,
ves_icall_System_GC_GetCollectionCount,
// token 643,
ves_icall_System_GC_register_ephemeron_array_raw,
// token 644,
ves_icall_System_GC_get_ephemeron_tombstone_raw,
// token 647,
ves_icall_System_GC_SuppressFinalize_raw,
// token 649,
ves_icall_System_GC_ReRegisterForFinalize_raw,
// token 651,
ves_icall_System_GC_GetGCMemoryInfo,
// token 653,
ves_icall_System_GC_AllocPinnedArray_raw,
// token 658,
ves_icall_System_Object_MemberwiseClone_raw,
// token 666,
ves_icall_System_Math_Acos,
// token 667,
ves_icall_System_Math_Acosh,
// token 668,
ves_icall_System_Math_Asin,
// token 669,
ves_icall_System_Math_Asinh,
// token 670,
ves_icall_System_Math_Atan,
// token 671,
ves_icall_System_Math_Atan2,
// token 672,
ves_icall_System_Math_Atanh,
// token 673,
ves_icall_System_Math_Cbrt,
// token 674,
ves_icall_System_Math_Ceiling,
// token 675,
ves_icall_System_Math_Cos,
// token 676,
ves_icall_System_Math_Cosh,
// token 677,
ves_icall_System_Math_Exp,
// token 678,
ves_icall_System_Math_Floor,
// token 679,
ves_icall_System_Math_Log,
// token 680,
ves_icall_System_Math_Log10,
// token 681,
ves_icall_System_Math_Pow,
// token 682,
ves_icall_System_Math_Sin,
// token 684,
ves_icall_System_Math_Sinh,
// token 685,
ves_icall_System_Math_Sqrt,
// token 686,
ves_icall_System_Math_Tan,
// token 687,
ves_icall_System_Math_Tanh,
// token 688,
ves_icall_System_Math_FusedMultiplyAdd,
// token 689,
ves_icall_System_Math_Log2,
// token 690,
ves_icall_System_Math_ModF,
// token 787,
ves_icall_System_MathF_Acos,
// token 788,
ves_icall_System_MathF_Acosh,
// token 789,
ves_icall_System_MathF_Asin,
// token 790,
ves_icall_System_MathF_Asinh,
// token 791,
ves_icall_System_MathF_Atan,
// token 792,
ves_icall_System_MathF_Atan2,
// token 793,
ves_icall_System_MathF_Atanh,
// token 794,
ves_icall_System_MathF_Cbrt,
// token 795,
ves_icall_System_MathF_Ceiling,
// token 796,
ves_icall_System_MathF_Cos,
// token 797,
ves_icall_System_MathF_Cosh,
// token 798,
ves_icall_System_MathF_Exp,
// token 799,
ves_icall_System_MathF_Floor,
// token 800,
ves_icall_System_MathF_Log,
// token 801,
ves_icall_System_MathF_Log10,
// token 802,
ves_icall_System_MathF_Pow,
// token 803,
ves_icall_System_MathF_Sin,
// token 805,
ves_icall_System_MathF_Sinh,
// token 806,
ves_icall_System_MathF_Sqrt,
// token 807,
ves_icall_System_MathF_Tan,
// token 808,
ves_icall_System_MathF_Tanh,
// token 809,
ves_icall_System_MathF_FusedMultiplyAdd,
// token 810,
ves_icall_System_MathF_Log2,
// token 811,
ves_icall_System_MathF_ModF,
// token 878,
ves_icall_RuntimeMethodHandle_ReboxFromNullable_raw,
// token 879,
ves_icall_RuntimeMethodHandle_ReboxToNullable_raw,
// token 948,
ves_icall_RuntimeType_GetCorrespondingInflatedMethod_raw,
// token 955,
ves_icall_RuntimeType_make_array_type_raw,
// token 958,
ves_icall_RuntimeType_make_byref_type_raw,
// token 960,
ves_icall_RuntimeType_make_pointer_type_raw,
// token 966,
ves_icall_RuntimeType_MakeGenericType_raw,
// token 967,
ves_icall_RuntimeType_GetMethodsByName_native_raw,
// token 969,
ves_icall_RuntimeType_GetPropertiesByName_native_raw,
// token 970,
ves_icall_RuntimeType_GetConstructors_native_raw,
// token 974,
ves_icall_RuntimeType_GetInterfaceMapData_raw,
// token 976,
ves_icall_RuntimeType_GetPacking_raw,
// token 979,
ves_icall_System_RuntimeType_CreateInstanceInternal_raw,
// token 980,
ves_icall_System_RuntimeType_AllocateValueType_raw,
// token 982,
ves_icall_RuntimeType_GetDeclaringMethod_raw,
// token 984,
ves_icall_System_RuntimeType_getFullName_raw,
// token 985,
ves_icall_RuntimeType_GetGenericArgumentsInternal_raw,
// token 988,
ves_icall_RuntimeType_GetGenericParameterPosition,
// token 989,
ves_icall_RuntimeType_GetEvents_native_raw,
// token 990,
ves_icall_RuntimeType_GetFields_native_raw,
// token 993,
ves_icall_RuntimeType_GetInterfaces_raw,
// token 995,
ves_icall_RuntimeType_GetNestedTypes_native_raw,
// token 998,
ves_icall_RuntimeType_GetDeclaringType_raw,
// token 1000,
ves_icall_RuntimeType_GetName_raw,
// token 1002,
ves_icall_RuntimeType_GetNamespace_raw,
// token 1009,
ves_icall_RuntimeType_IsUnmanagedFunctionPointerInternal,
// token 1014,
ves_icall_RuntimeType_FunctionPointerReturnAndParameterTypes_raw,
// token 1088,
ves_icall_RuntimeTypeHandle_GetAttributes,
// token 1090,
ves_icall_RuntimeTypeHandle_GetMetadataToken_raw,
// token 1092,
ves_icall_RuntimeTypeHandle_GetGenericTypeDefinition_impl_raw,
// token 1102,
ves_icall_RuntimeTypeHandle_GetCorElementType,
// token 1103,
ves_icall_RuntimeTypeHandle_HasInstantiation,
// token 1104,
ves_icall_RuntimeTypeHandle_IsComObject_raw,
// token 1105,
ves_icall_RuntimeTypeHandle_IsInstanceOfType_raw,
// token 1107,
ves_icall_RuntimeTypeHandle_HasReferences_raw,
// token 1114,
ves_icall_RuntimeTypeHandle_GetArrayRank_raw,
// token 1115,
ves_icall_RuntimeTypeHandle_GetAssembly_raw,
// token 1116,
ves_icall_RuntimeTypeHandle_GetElementType_raw,
// token 1117,
ves_icall_RuntimeTypeHandle_GetModule_raw,
// token 1118,
ves_icall_RuntimeTypeHandle_GetBaseType_raw,
// token 1126,
ves_icall_RuntimeTypeHandle_type_is_assignable_from_raw,
// token 1127,
ves_icall_RuntimeTypeHandle_IsGenericTypeDefinition,
// token 1128,
ves_icall_RuntimeTypeHandle_GetGenericParameterInfo_raw,
// token 1132,
ves_icall_RuntimeTypeHandle_is_subclass_of_raw,
// token 1133,
ves_icall_RuntimeTypeHandle_IsByRefLike_raw,
// token 1136,
ves_icall_System_RuntimeTypeHandle_internal_from_name_raw,
// token 1140,
ves_icall_System_String_FastAllocateString_raw,
// token 1141,
ves_icall_System_String_InternalIsInterned_raw,
// token 1142,
ves_icall_System_String_InternalIntern_raw,
// token 1426,
ves_icall_System_Type_internal_from_handle_raw,
// token 1645,
ves_icall_System_ValueType_InternalGetHashCode_raw,
// token 1646,
ves_icall_System_ValueType_Equals_raw,
// token 9738,
ves_icall_System_Threading_Interlocked_CompareExchange_Int,
// token 9739,
ves_icall_System_Threading_Interlocked_CompareExchange_Object,
// token 9741,
ves_icall_System_Threading_Interlocked_Decrement_Int,
// token 9742,
ves_icall_System_Threading_Interlocked_Decrement_Long,
// token 9743,
ves_icall_System_Threading_Interlocked_Increment_Int,
// token 9744,
ves_icall_System_Threading_Interlocked_Increment_Long,
// token 9745,
ves_icall_System_Threading_Interlocked_Exchange_Int,
// token 9746,
ves_icall_System_Threading_Interlocked_Exchange_Object,
// token 9748,
ves_icall_System_Threading_Interlocked_CompareExchange_Long,
// token 9750,
ves_icall_System_Threading_Interlocked_Exchange_Long,
// token 9752,
ves_icall_System_Threading_Interlocked_Read_Long,
// token 9753,
ves_icall_System_Threading_Interlocked_Add_Int,
// token 9754,
ves_icall_System_Threading_Interlocked_Add_Long,
// token 9765,
ves_icall_System_Threading_Monitor_Monitor_Enter_raw,
// token 9767,
mono_monitor_exit_icall_raw,
// token 9772,
ves_icall_System_Threading_Monitor_Monitor_pulse_raw,
// token 9774,
ves_icall_System_Threading_Monitor_Monitor_pulse_all_raw,
// token 9776,
ves_icall_System_Threading_Monitor_Monitor_wait_raw,
// token 9778,
ves_icall_System_Threading_Monitor_Monitor_try_enter_with_atomic_var_raw,
// token 9828,
ves_icall_System_Threading_Thread_StartInternal_raw,
// token 9834,
ves_icall_System_Threading_Thread_InitInternal_raw,
// token 9835,
ves_icall_System_Threading_Thread_GetCurrentThread,
// token 9837,
ves_icall_System_Threading_InternalThread_Thread_free_internal_raw,
// token 9838,
ves_icall_System_Threading_Thread_GetState_raw,
// token 9839,
ves_icall_System_Threading_Thread_SetState_raw,
// token 9840,
ves_icall_System_Threading_Thread_ClrState_raw,
// token 9841,
ves_icall_System_Threading_Thread_SetName_icall_raw,
// token 9843,
ves_icall_System_Threading_Thread_YieldInternal,
// token 9845,
ves_icall_System_Threading_Thread_SetPriority_raw,
// token 10954,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_PrepareForAssemblyLoadContextRelease_raw,
// token 10958,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_GetLoadContextForAssembly_raw,
// token 10960,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFile_raw,
// token 10961,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalInitializeNativeALC_raw,
// token 10962,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFromStream_raw,
// token 10963,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalGetLoadedAssemblies_raw,
// token 11230,
ves_icall_System_GCHandle_InternalAlloc_raw,
// token 11231,
ves_icall_System_GCHandle_InternalFree_raw,
// token 11232,
ves_icall_System_GCHandle_InternalGet_raw,
// token 11233,
ves_icall_System_GCHandle_InternalSet_raw,
// token 11254,
ves_icall_System_Runtime_InteropServices_Marshal_GetLastPInvokeError,
// token 11255,
ves_icall_System_Runtime_InteropServices_Marshal_SetLastPInvokeError,
// token 11256,
ves_icall_System_Runtime_InteropServices_Marshal_StructureToPtr_raw,
// token 11258,
ves_icall_System_Runtime_InteropServices_Marshal_PtrToStructureInternal_raw,
// token 11260,
ves_icall_System_Runtime_InteropServices_Marshal_SizeOfHelper_raw,
// token 11312,
ves_icall_System_Runtime_InteropServices_NativeLibrary_LoadByName_raw,
// token 11392,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalGetHashCode_raw,
// token 11394,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalTryGetHashCode_raw,
// token 11396,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetObjectValue_raw,
// token 11406,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetUninitializedObjectInternal_raw,
// token 11407,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InitializeArray_raw,
// token 11408,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetSpanDataFrom_raw,
// token 11409,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_RunClassConstructor_raw,
// token 11410,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_SufficientExecutionStack,
// token 11874,
ves_icall_System_Reflection_Assembly_GetExecutingAssembly_raw,
// token 11875,
ves_icall_System_Reflection_Assembly_GetEntryAssembly_raw,
// token 11880,
ves_icall_System_Reflection_Assembly_InternalLoad_raw,
// token 11881,
ves_icall_System_Reflection_Assembly_InternalGetType_raw,
// token 11920,
ves_icall_System_Reflection_AssemblyName_GetNativeName,
// token 11956,
ves_icall_MonoCustomAttrs_GetCustomAttributesInternal_raw,
// token 11963,
ves_icall_MonoCustomAttrs_GetCustomAttributesDataInternal_raw,
// token 11970,
ves_icall_MonoCustomAttrs_IsDefinedInternal_raw,
// token 11981,
ves_icall_System_Reflection_FieldInfo_internal_from_handle_type_raw,
// token 11985,
ves_icall_System_Reflection_FieldInfo_get_marshal_info_raw,
// token 12009,
ves_icall_System_Reflection_LoaderAllocatorScout_Destroy,
// token 12092,
ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceNames_raw,
// token 12094,
ves_icall_System_Reflection_RuntimeAssembly_GetExportedTypes_raw,
// token 12105,
ves_icall_System_Reflection_RuntimeAssembly_GetInfo_raw,
// token 12107,
ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceInternal_raw,
// token 12108,
ves_icall_System_Reflection_Assembly_GetManifestModuleInternal_raw,
// token 12109,
ves_icall_System_Reflection_RuntimeAssembly_GetModulesInternal_raw,
// token 12116,
ves_icall_System_Reflection_RuntimeCustomAttributeData_ResolveArgumentsInternal_raw,
// token 12131,
ves_icall_RuntimeEventInfo_get_event_info_raw,
// token 12151,
ves_icall_reflection_get_token_raw,
// token 12152,
ves_icall_System_Reflection_EventInfo_internal_from_handle_type_raw,
// token 12160,
ves_icall_RuntimeFieldInfo_ResolveType_raw,
// token 12162,
ves_icall_RuntimeFieldInfo_GetParentType_raw,
// token 12169,
ves_icall_RuntimeFieldInfo_GetFieldOffset_raw,
// token 12170,
ves_icall_RuntimeFieldInfo_GetValueInternal_raw,
// token 12173,
ves_icall_RuntimeFieldInfo_SetValueInternal_raw,
// token 12175,
ves_icall_RuntimeFieldInfo_GetRawConstantValue_raw,
// token 12180,
ves_icall_reflection_get_token_raw,
// token 12186,
ves_icall_get_method_info_raw,
// token 12187,
ves_icall_get_method_attributes,
// token 12194,
ves_icall_System_Reflection_MonoMethodInfo_get_parameter_info_raw,
// token 12196,
ves_icall_System_MonoMethodInfo_get_retval_marshal_raw,
// token 12208,
ves_icall_System_Reflection_RuntimeMethodInfo_GetMethodFromHandleInternalType_native_raw,
// token 12211,
ves_icall_RuntimeMethodInfo_get_name_raw,
// token 12212,
ves_icall_RuntimeMethodInfo_get_base_method_raw,
// token 12213,
ves_icall_reflection_get_token_raw,
// token 12224,
ves_icall_InternalInvoke_raw,
// token 12233,
ves_icall_RuntimeMethodInfo_GetPInvoke_raw,
// token 12239,
ves_icall_RuntimeMethodInfo_MakeGenericMethod_impl_raw,
// token 12240,
ves_icall_RuntimeMethodInfo_GetGenericArguments_raw,
// token 12241,
ves_icall_RuntimeMethodInfo_GetGenericMethodDefinition_raw,
// token 12243,
ves_icall_RuntimeMethodInfo_get_IsGenericMethodDefinition_raw,
// token 12244,
ves_icall_RuntimeMethodInfo_get_IsGenericMethod_raw,
// token 12261,
ves_icall_InvokeClassConstructor_raw,
// token 12263,
ves_icall_InternalInvoke_raw,
// token 12277,
ves_icall_reflection_get_token_raw,
// token 12300,
ves_icall_System_Reflection_RuntimeModule_InternalGetTypes_raw,
// token 12301,
ves_icall_System_Reflection_RuntimeModule_GetGuidInternal_raw,
// token 12302,
ves_icall_System_Reflection_RuntimeModule_ResolveMethodToken_raw,
// token 12329,
ves_icall_RuntimePropertyInfo_get_property_info_raw,
// token 12359,
ves_icall_reflection_get_token_raw,
// token 12360,
ves_icall_System_Reflection_RuntimePropertyInfo_internal_from_handle_type_raw,
// token 13019,
ves_icall_CustomAttributeBuilder_GetBlob_raw,
// token 13033,
ves_icall_DynamicMethod_create_dynamic_method_raw,
// token 13127,
ves_icall_AssemblyBuilder_basic_init_raw,
// token 13128,
ves_icall_AssemblyBuilder_UpdateNativeCustomAttributes_raw,
// token 13348,
ves_icall_ModuleBuilder_basic_init_raw,
// token 13349,
ves_icall_ModuleBuilder_set_wrappers_type_raw,
// token 13357,
ves_icall_ModuleBuilder_getUSIndex_raw,
// token 13358,
ves_icall_ModuleBuilder_getToken_raw,
// token 13359,
ves_icall_ModuleBuilder_getMethodToken_raw,
// token 13365,
ves_icall_ModuleBuilder_RegisterToken_raw,
// token 13464,
ves_icall_TypeBuilder_create_runtime_class_raw,
// token 13963,
ves_icall_System_IO_Stream_HasOverriddenBeginEndRead_raw,
// token 13964,
ves_icall_System_IO_Stream_HasOverriddenBeginEndWrite_raw,
// token 14373,
ves_icall_System_Diagnostics_Debugger_IsAttached_internal,
// token 14374,
ves_icall_System_Diagnostics_Debugger_Log,
// token 14379,
ves_icall_System_Diagnostics_StackFrame_GetFrameInfo,
// token 14389,
ves_icall_System_Diagnostics_StackTrace_GetTrace,
// token 15771,
ves_icall_Mono_RuntimeClassHandle_GetTypeFromClass,
// token 15792,
ves_icall_Mono_RuntimeGPtrArrayHandle_GPtrArrayFree,
// token 15794,
ves_icall_Mono_SafeStringMarshal_StringToUtf8,
// token 15796,
ves_icall_Mono_SafeStringMarshal_GFree,
};
static uint8_t corlib_icall_flags [] = {
0,
0,
0,
0,
0,
4,
4,
0,
4,
0,
4,
4,
4,
0,
0,
0,
4,
4,
4,
4,
4,
0,
4,
0,
0,
0,
4,
0,
4,
4,
4,
4,
0,
4,
4,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
0,
4,
0,
4,
4,
0,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
0,
0,
0,
0,
0,
0,
0,
};
